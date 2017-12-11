using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public static class ObjectValidationsExtensions
    {
        /// <summary>
        /// Adds the validation Func/method to entire class
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="objectValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IObjectValidations<TSource> Add<TSource>(this IObjectValidations<TSource> objectValidations,
            Func<TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TSource, ValidationContext, ValidationResult> map = (source, _) => validation(source);
            return Add(objectValidations, map, setter);
        }

        /// <summary>
        /// Adds the validation Func/method to entire class
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="objectValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IObjectValidations<TSource> Add<TSource>(this IObjectValidations<TSource> objectValidations,
            Func<TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<object, ValidationContext, ValidationResult> map =
                (source, context) => validation((TSource) source, context);

            var validationAttribute = new ValidatableObjectAttribute(map);

            return objectValidations.Add(validationAttribute, setter);
        }

        /// <summary>
        /// Factory method to create the attribute
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="objectValidations"></param>
        /// <param name="factory"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IObjectValidations<TSource> Add<TSource, TValidationAttribute>(
            this IObjectValidations<TSource> objectValidations, Func<TValidationAttribute> factory,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return objectValidations.Add(factory(), setter);
        }
    }
}
