using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class ValidatableObjectAttribute : ValidationAttribute
    {
        private readonly Func<object, ValidationContext, ValidationResult> _isValid;
        public ValidatableObjectAttribute(Func<object, ValidationContext, ValidationResult> isValid)
        {
            _isValid = isValid;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return _isValid(value, validationContext);
        }

        public override bool RequiresValidationContext => true;
    }
}
