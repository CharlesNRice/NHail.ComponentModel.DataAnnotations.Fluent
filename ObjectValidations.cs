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
        public ObjectValidations(IAttributes<TSource> provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Provider = provider;
        }

        public IObjectValidations<TSource> Add(Func<TSource, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TSource, ValidationContext, ValidationResult> mapper = (source, context) => validation(source);
            return Add(mapper);
        }

        public IObjectValidations<TSource> Add(Func<TSource, ValidationContext, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<object, ValidationContext, ValidationResult> mapper =
                (obj, context) => validation((TSource)obj, context);
            Provider.AddAttributes(new ValidatableObjectAttribute(mapper));
            return this;
        }

        public IObjectValidations<TSource> Add(Func<ValidationAttribute> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            Provider.AddAttributes(factory());
            return this;
        }

        public IObjectValidations<TSource> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null) where TValidationAttribute : ValidationAttribute, new()
        {
            var attribute = new TValidationAttribute();
            setter?.Invoke(attribute);
            Provider.AddAttributes(attribute);
            return this;
        }

        public IAttributes<TSource> Provider { get; }

    }
}
