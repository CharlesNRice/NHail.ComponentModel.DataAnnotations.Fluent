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

        /// <summary>
        /// Adds the validation attribute to the property
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="validationAttribute"></param>
        /// <returns></returns>
        public IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            TValidationAttribute validationAttribute)
            where TValidationAttribute : ValidationAttribute
        {
            _provider.AddPropertyAttributes(_property, validationAttribute);
            return this;
        }

        /// <summary>
        /// Helper method to chain into Add(TValidationAttribute validationAttribute) but don't need to create attribute
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="setter"></param>
        /// <returns></returns>
        public IPropertyValidations<TSource, TProperty> Add<TValidationAttribute>(
            Action<TValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute, new()
        {
            var attribute = new TValidationAttribute();
            setter?.Invoke(attribute);
            return Add(attribute);
        }

        /// <summary>
        /// Helper method to chain into Add(TValidationAttribute validationAttribute) but don't need to create attribute
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IPropertyValidations<TSource, TProperty> AddIfValid<TValidationAttribute>(
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute, new()
        {
            return AddIfValid<TValidationAttribute>(null, properties);
        }

        /// <summary>
        /// Helper method to chain into Add(TValidationAttribute validationAttribute) but don't need to create attribute
        /// </summary>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="setter"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IPropertyValidations<TSource, TProperty> AddIfValid<TValidationAttribute>(
            Action<TValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute, new()
        {
            var attribute = new TValidationAttribute();
            setter?.Invoke(attribute);

            var propsToValidate = properties.ToDictionary(p => p.NameOf(), p =>
            {
                var func = p.Compile();
                Func<object, object> convert = o => func((TSource) o);
                return convert;
            });

            var validationAttribute = new ValidateIfAttribute(propsToValidate, attribute);

            return Add(validationAttribute);
        }
    }
}
