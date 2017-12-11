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
        /// Adds the attribute to the class 
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="validationAttribute"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IObjectValidations<TSource> Add<TValidationAttribute>(TValidationAttribute validationAttribute,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute;
    }
}
