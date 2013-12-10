using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PinkTravel.Localization.ModelProviders
{
    public class LocalizableDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        public LocalizableDataAnnotationsModelValidatorProvider()
            : base()
        {
            
        }

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            var validators = base.GetValidators(metadata, context, attributes);
            return validators.Select(validator => new LocalizableDataAnnotationsModelValidator(validator, metadata, context)).ToList();
        }
    }
}
