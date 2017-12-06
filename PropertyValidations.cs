using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class PropertyValidations<TSource, TProperty> : IPropertyValidations<TSource, TProperty>
    {
        private readonly Expression<Func<TSource, TProperty>> _property;
        private readonly IAttributeConfiguration<TSource> _provider;

        public PropertyValidations(IAttributeConfiguration<TSource> provider,
            Expression<Func<TSource, TProperty>> property)
        {
            _provider = provider;
            _property = property;
        }

        public IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Func<TValidationAttribute, Attribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new()
        {
            return Add(() => new TValidationAttribute(), setter);
        }

        public IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(Func<TValidationAttribute> factory,
            Func<TValidationAttribute, Attribute> setter = null)
            where TValidationAttribute : ValidationAttribute
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (setter == null)
            {
                setter = validAttribute => validAttribute;
            }

            // If factory returns null or setter returns null then don't add attribute
            var validationAttribute = factory();
            if (validationAttribute != null)
            {
                var attribute = setter(validationAttribute);
                if (attribute != null)
                {
                    _provider.AddPropertyAttributes(_property, attribute);
                }
            }

            return this;
        }

        public IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }
            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property);
            return Add(mapper, setter);
        }

        public IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property, context);
            return Add(mapper, setter);
        }

        public IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property, source);
            return Add(mapper, setter);
        }

        public IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            Func<ValidationAttribute, Attribute> setter = null)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            if (setter == null)
            {
                setter = validationAttribute => validationAttribute;
            }

            Func<object, ValidationContext, ValidationResult> mapper =
                (value, context) => validation((TProperty) value, (TSource) context.ObjectInstance, context);

            return Add(() => new ValidatableObjectAttribute(mapper), setter);
        }
    }
}
