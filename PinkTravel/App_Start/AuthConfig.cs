using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using PinkTravel.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace PinkTravel
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings["Facebook:AppId"],
                appSecret: ConfigurationManager.AppSettings["Facebook:AppSecret"]);

            //OAuthWebSecurity.RegisterGoogleClient();

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "LoginName", true);
        }
    }
}
