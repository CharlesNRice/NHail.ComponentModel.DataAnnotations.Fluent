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
        /// <summary>
        /// Adds a validation attribute to the given property
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Func<TValidationAttribute, Attribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();

        /// <summary>
        /// Adds a validation attribute to the given property (use when no default constructor)
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="factory"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Func<TValidationAttribute, Attribute> setter = null)
            where TValidationAttribute : ValidationAttribute;

        /// <summary>
        /// Wraps a method or func to a ValidatonAttribute
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null);

        /// <summary>
        /// Wraps a method or func to a ValidatonAttribute
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null);

        /// <summary>
        /// Wraps a method or func to a ValidatonAttribute
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null);

        /// <summary>
        /// Wraps a method or func to a ValidatonAttribute
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null);
    }
}
