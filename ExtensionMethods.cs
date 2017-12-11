using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public static class ExtensionMethods
    {

        public static IPropertyValidations<TSource, TProperty> ValidationsFor<TSource, TProperty>(
            this IAttributeConfiguration<TSource> provider, Expression<Func<TSource, TProperty>> property)
        {
            return new PropertyValidations<TSource, TProperty>(provider, property);
        }

        public static IObjectValidations<TSource> Validations<TSource>(
            this IAttributeConfiguration<TSource> provider)
        {
            return new ObjectValidations<TSource>(provider);
        }

        public static string NameOf<TSource, TProperty>(this Expression<Func<TSource, TProperty>> property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var member = property.Body as MemberExpression;
            if (member == null)
            {
                var ubody = property.Body as UnaryExpression;
                member = ubody?.Operand as MemberExpression;
            }
            var propinfo = member?.Member as PropertyInfo;
            var propName = propinfo?.Name;
            if (propName == null)
            {
                throw new ArgumentException("Invalid Property Expression", nameof(property));
            }

            return propName;
        }
    }
}
