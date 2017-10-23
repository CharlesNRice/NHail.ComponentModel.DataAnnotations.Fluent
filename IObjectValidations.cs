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
        IObjectValidations<TSource> Add(Func<ValidationAttribute> factory);
        IObjectValidations<TSource> Add(Func<TSource, ValidationResult> validation);
        IObjectValidations<TSource> Add(Func<TSource, ValidationContext, ValidationResult> validation);
        IAttributes<TSource> Provider { get; }
    }
}
