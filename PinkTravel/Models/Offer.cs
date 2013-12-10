using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinkTravel.Models
{
	public class Offer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OfferId { get; set; }

		[Required]
		[DisplayName("Hotel")]
		public string HotelName { get; set; }

		[Required]
		[DisplayName("Location")]
		public string LocationName { get; set; }

		[DisplayName("Hotel image")]
		[ForeignKey("HotelImageId")]
		public virtual ImageModel HotelImage { get; set; }

		public int HotelImageId { get; set; }

		[DisplayName("Location image")]
		[ForeignKey("LocationImageId")]
		public virtual ImageModel LocationImage { get; set; }

		public int LocationImageId { get; set; }

		public DateTime AddedDate { get; set; }
	}
}