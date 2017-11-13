using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public interface IObjectValidations<TSource>
    {
        IObjectValidations<TSource> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();

        IObjectValidations<TSource> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute;

        IObjectValidations<TSource> Add(Func<TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter = null);

        IObjectValidations<TSource> Add(Func<TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter = null);

        IAttributeConfiguration<TSource> Provider { get; }
    }
}
