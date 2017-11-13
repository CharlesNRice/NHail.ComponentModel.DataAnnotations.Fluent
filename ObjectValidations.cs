using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class ObjectValidations<TSource> : IObjectValidations<TSource>
    {
        public ObjectValidations(IAttributeConfiguration<TSource> provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Provider = provider;
        }

        public IObjectValidations<TSource> Add(Func<TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TSource, ValidationContext, ValidationResult> mapper = (source, context) => validation(source);
            return Add(mapper, setter);
        }

        public IObjectValidations<TSource> Add(Func<TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<object, ValidationContext, ValidationResult> mapper =
                (obj, context) => validation((TSource) obj, context);
            return Add(() => new ValidatableObjectAttribute(mapper), setter);
        }

        public IObjectValidations<TSource> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            var validation = factory();
            setter?.Invoke(validation);
            Provider.AddAttributes(validation);
            return this;
        }

        public IObjectValidations<TSource> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new()
        {
            return Add(() => new TValidationAttribute(), setter);
        }

        public IAttributeConfiguration<TSource> Provider { get; }

    }
}
