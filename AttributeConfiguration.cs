using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class AttributeConfiguration<TSource> : IAttributeConfiguration<TSource>
    {
        private readonly AttributeProvider<TSource> _provider;

        public AttributeConfiguration() : this(new AttributeProvider<TSource>())
        {
        }

        public AttributeConfiguration(AttributeProvider<TSource> provider)
        {
            _provider = provider;
        }

        public IAttributeConfiguration<TSource> AddPropertyAttributes(string propertyName, params Attribute[] validations)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            foreach (var validation in validations)
            {
                _provider.AddPropertyAttribute(propertyName, validation);
            }
            return this;
        }

        public IAttributeConfiguration<TSource> AddPropertyAttributes<TProperty>(Expression<Func<TSource, TProperty>> property, params Attribute[] validations)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var propName = property.NameOf();
            return AddPropertyAttributes(propName, validations);
        }

        public IAttributeConfiguration<TSource> AddAttributes(params Attribute[] validations)
        {
            foreach (var validation in validations)
            {
                _provider.AddAttribute(validation);
            }
            return this;
        }
    }

}
