using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PinkTravel.Models
{
	public class BaseModel
	{
		protected BaseModel()
    {
        // apply any DefaultValueAttribute settings to their properties
        var propertyInfos = this.GetType().GetProperties();
        foreach (var propertyInfo in propertyInfos)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
            if (attributes.Any())
            {
                var attribute = (DefaultValueAttribute) attributes[0];
                propertyInfo.SetValue(this, attribute.Value, null);
            }
        }
    }
	}
}