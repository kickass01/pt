using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PinkTravel.Helper;
using PinkTravel.Models;

namespace PinkTravel.Controllers
{
	public class PImagesController : Controller
	{
		//
		// GET: /PImages/

		private readonly JavaScriptSerializer _jsonSerializer = new JavaScriptSerializer();

		private class FilesWrapper
		{
			public List<FilesStatus> files;
		}

		[HttpPost]
		//[AuthorizeAdminOnly]
		public ActionResult UploadImage(ImageModel model)
		{
			if (Request.Files.Count < 1 || Request.Files[0].ContentLength == 0)
				return new HttpStatusCodeResult(403);

			var files = new List<FilesStatus>();

			var file = Request.Files[0];
			if (file != null && !Regex.IsMatch(file.ContentType, "^image/"))
			{
				return new HttpStatusCodeResult(403);
			}
			try
			{
				// Get and add cropped image
				ImageFormat imageFormat = GetImageFormat(file);
				var croppedImageBytes = GetCroppedImage(model, file, imageFormat);
				string croppedImageName = string.Empty;
				if (croppedImageBytes != null)
				{
					croppedImageName = Constants.CroppedImagePrefix + file.FileName;
					Session.Add(croppedImageName, croppedImageBytes);
				}

				var originalImageBytes = new byte[file.ContentLength];
				file.InputStream.Seek(0, SeekOrigin.Begin);
				file.InputStream.Read(originalImageBytes, 0, file.ContentLength);
				Session.Add(file.FileName, originalImageBytes);

				string thumbnailName;
				var thumbImg = GetThumbImage(croppedImageBytes ?? originalImageBytes, imageFormat, file, out thumbnailName);
				Session.Add(thumbnailName, thumbImg);

				files.Add(new FilesStatus
				{
					size = originalImageBytes.Length,
					name = file.FileName,
					url = Url.Action("Image", new { session = true, name = file.FileName + "/" }),
					thumbnail_url = Url.Action("Image", new { session = true, name = thumbnailName + "/" }),
					type = file.ContentType,
					cropped_image_name = croppedImageName,
					cropped_image_size = croppedImageBytes != null ? croppedImageBytes.Length : 0

				});
			}
			catch (Exception ex)
			{
				files.Add(new FilesStatus
				{
					name = file.FileName,
					error = ex.Message,
				});
			}

			var json = _jsonSerializer.Serialize(new FilesWrapper { files = files });
			return Json(json);
		}

		private ImageFormat GetImageFormat(HttpPostedFileBase file)
		{
			using (var imgInput = System.Drawing.Image.FromStream(file.InputStream))
			{
				return imgInput.RawFormat;
			}
		}

		private byte[] GetThumbImage(byte[] croppedImage, ImageFormat imageFormat, HttpPostedFileBase file,
			out string thumbnailName)
		{
			byte[] thumbImg;
			const int thumbWidth = 100;

			TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
			using (var src = (Bitmap)tc.ConvertFrom(croppedImage))
			{
				thumbImg = GetResizedImage(src, thumbWidth, 0, imageFormat);
			}

			thumbnailName = "_thumb" + file.FileName;
			return thumbImg;
		}

		private static byte[] GetCroppedImage(ImageModel model, HttpPostedFileBase file, ImageFormat imageFormat)
		{
			if (Math.Abs(model.CropX1 - model.CropX2) > 0 && Math.Abs(model.CropY1 - model.CropY2) > 0)
			{
				int width = Math.Abs(model.CropX2 - model.CropX1);
				int height = Math.Abs(model.CropY2 - model.CropY1);
				var cropRectangle = new Rectangle(model.CropX1, model.CropY1, width, height);
				byte[] croppedImage;
				using (var src = (Bitmap)System.Drawing.Image.FromStream(file.InputStream))
				using (var target = new Bitmap(cropRectangle.Width, cropRectangle.Height))
				{
					using (Graphics graphics = Graphics.FromImage(target))
					{
						graphics.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), cropRectangle,
							GraphicsUnit.Pixel);
					}

					using (var stream = new MemoryStream())
					{
						target.Save(stream, imageFormat);
						croppedImage = new byte[stream.Length];
						stream.Seek(0, SeekOrigin.Begin);
						stream.Read(croppedImage, 0, (int)stream.Length);
					}
				}

				return croppedImage;
			}

			return null;
		}

		private byte[] GetResizedImage(Bitmap imgIn, int width, int height, ImageFormat format)
		{
			double x = imgIn.Width;
			double y = imgIn.Height;

			double factor = 1;
			if (width > 0)
			{
				factor = width / x;
			}
			else if (height > 0)
			{
				factor = height / y;
			}

			using (var stream = new MemoryStream())
			{
				var imgOut = new Bitmap((int)(x * factor), (int)(y * factor));
				imgOut.SetResolution(72, 72);

				using (var graphics = Graphics.FromImage(imgOut))
				{
					graphics.Clear(Color.White);
					graphics.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)), new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);
					imgOut.Save(stream, format);
					stream.Seek(0, SeekOrigin.Begin);

					var result = new byte[stream.Length];
					stream.Read(result, 0, (int)stream.Length);
					return result;
				}
			}
		}

		[HttpGet]
		public ActionResult Image(string name)
		{
			byte[] content = null;
			if (Request.QueryString["session"] != null)
			{
				var bytes = Session[name] as byte[];
				if (bytes != null)
				{
					content = bytes;
				}
			}
			else
			{
				int offerId;
				bool cropped;
				bool.TryParse(Request.QueryString["nocrop"], out cropped);
				if (Request.QueryString["offer"] != null && Int32.TryParse(Request.QueryString["offer"], out offerId))
				{
					name = cropped ? Constants.CroppedImagePrefix + name : name; // If requested image is the uncropped version
					string filePath = Server.MapPath(Constants.ImagesBaseFolder + offerId + "/" + name);
					if (System.IO.File.Exists(filePath))
					{
						content = System.IO.File.ReadAllBytes(filePath);
					}
				}
			}

			if (content == null) return new HttpStatusCodeResult(404);

			Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", name));
			return File(content, "application/octet-stream");
		}

		public ActionResult Index()
		{
			return new EmptyResult();
		}
	}
}
