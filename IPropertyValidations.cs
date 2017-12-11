using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public interface IPropertyValidations<TSource, out TProperty>
    {
        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            TValidationAttribute validationAttribute)
            where TValidationAttribute : ValidationAttribute;

        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();

        IPropertyValidations<TSource, TProperty> AddIfValid<TValidationAttribute>(
            Action<TValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute, new();

        IPropertyValidations<TSource, TProperty> AddIfValid<TValidationAttribute>(
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute, new();
    }
}
