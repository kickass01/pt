using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PinkTravel.Helper;
using PinkTravel.Models;

namespace PinkTravel.Controllers
{
	public class OfferController : LocalizedController
	{
		private PinkTravelContext db = new PinkTravelContext();

		// GET: /Offer/
		public ActionResult Index()
		{
			return View(db.Offers.ToList());
		}

		//
		// GET: /Offer/Details/5

		public ActionResult Details(int id = 0)
		{
			Offer offer = db.Offers.Find(id);
			if (offer == null)
			{
				return HttpNotFound();
			}

			return View(offer);
		}

		//
		// GET: /Offer/Create

		//[AuthorizeAdminOnly]
		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Offer/Create

		[HttpPost]
		[ValidateAntiForgeryToken]
		//[AuthorizeAdminOnly]
		public ActionResult Create(Offer offer)
		{
			if (ModelState.IsValid)
			{
				offer.AddedDate = DateTime.Now.Date;
				db.Offers.Add(offer);
				db.SaveChanges();

				SaveOfferImagesToDisk(offer);

				return RedirectToAction("Index");
			}

			return View();
		}

		//[AuthorizeAdminOnly]
		public ActionResult Edit(int id = 0)
		{
			Offer offer = db.Offers.Find(id);
			if (offer == null)
			{
				return HttpNotFound();
			}

			return View(offer);
		}


		//
		// POST: /Offer/Edit/5

		[HttpPost]
		[ValidateAntiForgeryToken]
		//[AuthorizeAdminOnly]
		public ActionResult Edit(Offer offer)
		{
			if (ModelState.IsValid)
			{
				db.Entry(offer).State = EntityState.Modified;
				db.SaveChanges();

				SaveOfferImagesToDisk(offer);

				return RedirectToAction("Index");
			}

			return View(offer);
		}

		//
		// GET: /Offer/Delete/5

		//[AuthorizeAdminOnly]
		public ActionResult Delete(int id = 0)
		{
			Offer offer = db.Offers.Find(id);
			if (offer == null)
			{
				return HttpNotFound();
			}

			return View(offer);
		}

		//
		// POST: /Offer/Delete/5

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		//[AuthorizeAdminOnly]
		public ActionResult DeleteConfirmed(int id)
		{
			Offer offer = db.Offers.Find(id);
			ImageModel hotelImage = null, locationImage = null;

			if (offer.HotelImage != null)
				hotelImage = db.Images.Find(offer.HotelImage.FullImageName);

			if (offer.LocationImage != null)
				locationImage = db.Images.Find(offer.LocationImage.FullImageName);
			db.Offers.Remove(offer);
			db.SaveChanges();

			db.Images.Remove(hotelImage);
			db.Images.Remove(locationImage);

			db.SaveChanges();
			return RedirectToAction("Index");
		}

		private void SaveOfferImagesToDisk(Offer offer)
		{
			if (offer.HotelImage != null)
			{
				SaveFileFromSessionToDisk(offer.HotelImage.FullImageName, offer.OfferId);
				SaveFileFromSessionToDisk(offer.HotelImage.CroppedImageName, offer.OfferId);
			}

			if (offer.LocationImage != null)
			{
				SaveFileFromSessionToDisk(offer.LocationImage.FullImageName, offer.OfferId);
				SaveFileFromSessionToDisk(offer.LocationImage.CroppedImageName, offer.OfferId);
			}
		}

		public void SaveFileFromSessionToDisk(string fileName, int offerId)
		{
			if (string.IsNullOrEmpty(fileName))
				return;

			var content = Session[fileName] as byte[];
			if (content == null)
				throw new ArgumentOutOfRangeException("fileName", Resources.PinkTravel.OfferController_SaveFileFromSessionToDisk_Invalid_filename_or_session_expired);

			string directorypath = Server.MapPath(Constants.ImagesBaseFolder) + offerId + "/";
			Directory.CreateDirectory(directorypath);

			System.IO.File.WriteAllBytes(directorypath + fileName, content);
		}


		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}