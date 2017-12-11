using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class ObjectValidations<TSource> : IObjectValidations<TSource>
    {
        private readonly IAttributeConfiguration<TSource> _provider;

        public ObjectValidations(IAttributeConfiguration<TSource> provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _provider = provider;
        }

        public IObjectValidations<TSource> Add<TValidationAttribute>(TValidationAttribute validationAttribute,
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute
        {
            if (validationAttribute == null)
            {
                throw new ArgumentNullException(nameof(validationAttribute));
            }
            setter?.Invoke(validationAttribute);
            _provider.AddAttributes(validationAttribute);
            return this;
        }

        public IObjectValidations<TSource> Add<TValidationAttribute>(Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new()
        {
            return Add(new TValidationAttribute(), setter);
        }
    }
}
