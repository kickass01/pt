using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Configuration;
using System.Web;

namespace PinkTravel.Models
{
	public class ImageModel : BaseModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "ImageModel_FullImageName_Select_an_image")]
		public string FullImageName { get; set; }

		public string CroppedImageName { get; set; }

		public string ContentType { get; set; }

		public int CroppedImageSize { get; set; }

		public int FullImageSize { get; set; }

		[DefaultValue(0)]
		public int CropX1 { get; set; }

		[DefaultValue(0)]
		public int CropX2 { get; set; }

		[DefaultValue(0)]
		public int CropY1 { get; set; }

		[DefaultValue(0)]
		public int CropY2 { get; set; }
	}
}