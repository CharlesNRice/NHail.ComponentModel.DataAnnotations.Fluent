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

        public PropertyValidations(IAttributes<TSource> provider, Expression<Func<TSource, TProperty>> property)
        {
            Provider = provider;
            _property = property;
        }

        public IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new()
        {
            var attribute = new TValidationAttribute();
            setter?.Invoke(attribute);
            Provider.AddPropertyAttributes(_property, attribute);

            return this;
        }

        public IPropertyValidations<TSource, TProperty> Add(Func<ValidationAttribute> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            Provider.AddPropertyAttributes(_property, factory());

            return this;
        }

        public IPropertyValidations<TSource, TProperty> Add(Func<TProperty, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }
            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property);
            return Add(mapper);
        }

        public IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, ValidationContext, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property, context);
            return Add(mapper);
        }

        public IPropertyValidations<TSource, TProperty> Add(Func<TProperty, TSource, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<TProperty, TSource, ValidationContext, ValidationResult> mapper =
                (property, source, context) => validation(property, source);
            return Add(mapper);
        }

        public IPropertyValidations<TSource, TProperty> Add(
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            Func<object, ValidationContext, ValidationResult> mapper =
                (value, context) => validation((TProperty)value, (TSource)context.ObjectInstance, context);
            Provider.AddPropertyAttributes(_property, new ValidatableObjectAttribute(mapper));
            return this;
        }

        public IAttributes<TSource> Provider { get; }
    }
}
