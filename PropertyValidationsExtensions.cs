using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public static class PropertyValidationsExtensions
    {
        #region Add
        /// <summary>
        /// Adds a func/method to get called when this property is validated
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> Add<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, _, __) => validation(property);
            return Add(propertyValidations, map, setter);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> Add<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, source, _) => validation(property, source);
            return Add(propertyValidations, map, setter);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> Add<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter = null)
        {
            Func<object, ValidationContext, ValidationResult> map =
                (prop, context) => validation((TProperty) prop, (TSource) context.ObjectInstance, context);
            var attribute = new ValidatableObjectAttribute(map);
            setter?.Invoke(attribute);

            return propertyValidations.Add(attribute);
        }

        /// <summary>
        /// Adds validation attribute to the property using the factory func/method
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="factory"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> Add<TSource, TProperty, TValidationAttribute>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TValidationAttribute> factory,
            Action<ValidationAttribute> setter = null)
            where TValidationAttribute : ValidationAttribute
        {
            var attribute = factory();
            setter?.Invoke(attribute);

            return propertyValidations.Add(attribute);
        }
        #endregion Add

        #region AddIfValid
        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, ValidationResult> validation,
            params Expression<Func<TSource, object>>[] properties)
        {
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, _, __) => validation(property);
            return AddIfValid(propertyValidations, map, null, properties);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, ValidationResult> validation,
            Action<ValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
        {
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, _, __) => validation(property);
            return AddIfValid(propertyValidations, map, setter, properties);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationResult> validation,
            params Expression<Func<TSource, object>>[] properties)
        {
            // chain down since can't have default and params
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, source, _) => validation(property, source);
            return AddIfValid(propertyValidations, map, null, properties);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationResult> validation,
            Action<ValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
        {
            Func<TProperty, TSource, ValidationContext, ValidationResult> map =
                (property, source, _) => validation(property, source);
            return AddIfValid(propertyValidations, map, setter, properties);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            params Expression<Func<TSource, object>>[] properties)
        {
            // Chain down since you can't have a default and params 
            return AddIfValid(propertyValidations, validation, null, properties);
        }

        /// <summary>
        /// Adds a func/method to get called when this property is validated 
        /// If the other properties pass in pass validation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="validation"></param>
        /// <param name="setter"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TProperty, TSource, ValidationContext, ValidationResult> validation,
            Action<ValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
        {
            Func<object, ValidationContext, ValidationResult> map =
                (prop, context) => validation((TProperty) prop, (TSource) context.ObjectInstance, context);
            var attribute = new ValidatableObjectAttribute(map);
            setter?.Invoke(attribute);

            var propsToValidate = properties.ToDictionary(p => p.NameOf(), p =>
            {
                var func = p.Compile();
                Func<object, object> convert = o => func((TSource) o);
                return convert;
            });

            var validationAttribute = new ValidateIfAttribute(propsToValidate, attribute);
            
            return propertyValidations.Add(validationAttribute);
        }

        /// <summary>
        /// Adds validation to property using the factory method to get the attribute
        /// Will call validation if other properties passed in are valid
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="factory"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty, TValidationAttribute>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TValidationAttribute> factory,
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute
        {
            return AddIfValid(propertyValidations, factory, null, properties);
        }

        /// <summary>
        /// Adds validation to property using the factory method to get the attribute
        /// Will call validation if other properties passed in are valid
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TValidationAttribute"></typeparam>
        /// <param name="propertyValidations"></param>
        /// <param name="factory"></param>
        /// <param name="setter"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IPropertyValidations<TSource, TProperty> AddIfValid<TSource, TProperty, TValidationAttribute>(
            this IPropertyValidations<TSource, TProperty> propertyValidations,
            Func<TValidationAttribute> factory,
            Action<ValidationAttribute> setter,
            params Expression<Func<TSource, object>>[] properties)
            where TValidationAttribute : ValidationAttribute
        {
            var attribute = factory();
            setter?.Invoke(attribute);

            var propsToValidate = properties.ToDictionary(p => p.NameOf(), p =>
            {
                var func = p.Compile();
                Func<object, object> convert = o => func((TSource)o);
                return convert;
            });

            var validationAttribute = new ValidateIfAttribute(propsToValidate, attribute);

            return propertyValidations.Add(validationAttribute);
        }

        #endregion AddIfValid

    }
}
