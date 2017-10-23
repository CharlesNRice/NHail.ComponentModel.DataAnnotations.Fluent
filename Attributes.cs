using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class Attributes<TSource> : IAttributes<TSource>
    {
        private readonly AttributeProvider<TSource> _provider;

        public Attributes() : this(new AttributeProvider<TSource>())
        {
        }

        public Attributes(AttributeProvider<TSource> provider)
        {
            _provider = provider;
        }

        public IAttributes<TSource> AddPropertyAttributes<TProperty>(Expression<Func<TSource, TProperty>> property, params Attribute[] validations)
        {
            var member = property.Body as MemberExpression;
            var propinfo = member?.Member as PropertyInfo;
            var propName = propinfo?.Name;
            if (propName == null)
            {
                throw new ArgumentException("Invalid Property Expression", nameof(property));
            }

            foreach (var validation in validations)
            {
                _provider.AddPropertyAttribute(propName, validation);
            }
            return this;
        }

        public IAttributes<TSource> AddAttributes(params Attribute[] validations)
        {
            foreach (var validation in validations)
            {
                _provider.AddAttribute(validation);
            }
            return this;
        }
    }

}
