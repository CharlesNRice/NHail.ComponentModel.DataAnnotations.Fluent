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
        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();
        IPropertyValidations<TSource, TProperty> Add(Func<ValidationAttribute> factory);
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationResult> validation);
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationContext, ValidationResult> validation);
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationResult> validation);
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationContext, ValidationResult> validation);
        IAttributes<TSource> Provider { get; }
    }
}
