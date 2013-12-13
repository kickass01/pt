using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using PinkTravel.Helper;

namespace PinkTravel.Models
{
	public class Offer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OfferId { get; set; }

		[Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_HotelName_Hotel_name_is_required")]
        [LocalizedDisplayName(MessageResourceName = "Offer_HotelName_Hotel_name")]
		public string HotelName { get; set; }

		[Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_LocationName_Location_name_is_required")]
        [LocalizedDisplayName(MessageResourceName = "Offer_LocationName_Offer_LocationName_Location")]
		public string Location { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_Country_Country")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_Country_Country_must_be_specified")]
        public string Country { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_FromDate_From_date")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_FromDate_From_date_must_be_specified")]
        public DateTime? FromDate { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_ToDate_Until_date")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_EndDate_End_date_must_be_specified")]
        public DateTime? EndDate { get; set; }

	    [NotMapped]
	    [LocalizedDisplayName(MessageResourceName = "Offer_NumberOfNights_Duration"), DataType(DataType.Duration)]
	    public int? NumberOfNights
	    {
	        get
	        {
	            if (FromDate.HasValue && EndDate.HasValue)
	            {
	                return (EndDate - FromDate).Value.Days;
	            }

	            return null;
	        }
	    }

        [LocalizedDisplayName(MessageResourceName = "Offer_PriceFrom_Price_from")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_PriceFrom_Starting_price_is_required")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        public double PriceFrom { get; set; }

	    [LocalizedDisplayName(MessageResourceName = "Offer_HotelImage_Hotel_image")]
		[ForeignKey("HotelImageId")]
		public virtual ImageModel HotelImage { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_LocationImage_Location_image")]
		[ForeignKey("LocationImageId")]
		public virtual ImageModel LocationImage { get; set; }

		public int? LocationImageId { get; set; }

        public int? HotelImageId { get; set; }

		public DateTime AddedDate { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_HotelStars_HotelStars")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_HotelStars_Select_number_of_stars")]
        [Range(1, 5, ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_HotelStars__0__must_be_between__1__and__5__stars_")]
        public int? HotelStars { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_HotelWebSite_Hotel_web_site")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_HotelWebSite_Hotel_website_is_required_"), DataType(DataType.Url)]
        public string HotelWebSite { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_HotelDescription_Hotel_description")]
        [Required(ErrorMessageResourceType = typeof(ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_HotelDescription_Hotel_description_is_required_"), DataType(DataType.MultilineText)]
        public string HotelDescription { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_Transport_Transport_details")]
        [Required(ErrorMessageResourceType = typeof (ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_Transport_Transport_details_are_required_"), DataType(DataType.MultilineText)]
        public string TransportDetails { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_TransferDetails_Transfer_details")]
        [Required(ErrorMessageResourceType = typeof(ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_TransferDetails_Transfer_details_are_required"), DataType(DataType.MultilineText)]
        public string TransferDetails { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_RoomDetails_Room_details")]
        [Required(ErrorMessageResourceType = typeof(ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_RoomDetails_Room_details_must_be_specified"), DataType(DataType.MultilineText)]
        public string RoomDetails { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_MealDetails_Meal_details")]
        [Required(ErrorMessageResourceType = typeof(ResourcesPt.PinkTravel), ErrorMessageResourceName = "Offer_MealDetails_Meail_details_must_be_specified"), DataType(DataType.MultilineText)]
        public string MealDetails { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_Observations_Observations")]
        [DataType(DataType.MultilineText)]
        public string Observations { get; set; }

        [LocalizedDisplayName(MessageResourceName = "Offer_NotIncludedInOffer_Not_included")]
        public string NotIncludedInOffer { get; set; }
	}
}