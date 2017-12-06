using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public interface IObjectValidations<out TSource>
    {
        /// <summary>
        /// Adds a validaton attribute to the class using default constructor
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="setter"></param>
        /// <returns></returns>
        IObjectValidations<TSource> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new();

        /// <summary>
        /// Adds a validation attribute to the class using a factory to create the validation attribute
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="factory"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IObjectValidations<TSource> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute;

        /// <summary>
        /// Wraps a Func or Method into a validation attributes and adds to the class
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IObjectValidations<TSource> Add(Func<TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter = null);

        /// <summary>
        /// Wraps a Func or Method into a validation attributes and adds to the class
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IObjectValidations<TSource> Add(Func<TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter = null);
    }
}
