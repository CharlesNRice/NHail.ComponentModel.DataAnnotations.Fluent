using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class ValidateIfAttribute : ValidationAttribute
    {
        private readonly IDictionary<string, Func<object, object>> _properties;
        private readonly ValidationAttribute _validation;
        public ValidateIfAttribute(IDictionary<string, Func<object, object>> properties, ValidationAttribute validation)
        {
            _properties = properties;
            _validation = validation;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valid = _properties.All(p => Validator.TryValidateProperty(p.Value(validationContext.ObjectInstance),
                new ValidationContext(validationContext.ObjectInstance, validationContext.ServiceContainer,
                    validationContext.Items)
                {MemberName = p.Key}, null));

            return valid ? _validation.GetValidationResult(value, validationContext) : ValidationResult.Success;
        }

        public override bool RequiresValidationContext => true;
    }
}
