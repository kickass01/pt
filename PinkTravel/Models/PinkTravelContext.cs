using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PinkTravel.Models
{
    public class PinkTravelContext : DbContext
    {
        public PinkTravelContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<ExternalUserInformation> ExternalUsers { get; set; }

		public DbSet<ImageModel> Images { get; set; }
    }
}