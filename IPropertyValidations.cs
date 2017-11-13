using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public interface IPropertyValidations<TSource, out TProperty>
    {
        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Func<TValidationAttribute, ValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();

        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Func<TValidationAttribute, ValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute;

        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationResult> validation,
            Func<ValidationAttribute, ValidationAttribute> setter = null);

        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, ValidationAttribute> setter = null);

        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationResult> validation,
            Func<ValidationAttribute, ValidationAttribute> setter = null);

        IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, ValidationAttribute> setter = null);

        IAttributeConfiguration<TSource> Provider { get; }
    }
}
