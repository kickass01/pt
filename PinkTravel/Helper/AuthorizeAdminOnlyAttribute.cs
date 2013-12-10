using System.Web.Mvc;

namespace PinkTravel.Helper
{
    public class AuthorizeAdminOnlyAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminOnlyAttribute()
        {
            this.Roles = "Admin";
        }
    }
}