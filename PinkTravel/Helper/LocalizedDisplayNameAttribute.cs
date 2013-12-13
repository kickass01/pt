using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PinkTravel.Helper
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        [Localizable(false)]
        public string MessageResourceName { get; set; }

        public override string DisplayName
        {
            get
            {
                return ResourcesPt.PinkTravel.ResourceManager.GetString(MessageResourceName);
            }
        }
    }
}