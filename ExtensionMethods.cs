using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public static class ExtensionMethods
    {
        public static IPropertyValidations<TSource, TProperty> ValidationsFor<TSource, TProperty>(
            this IAttributes<TSource> provider, Expression<Func<TSource, TProperty>> property)
        {
            return new PropertyValidations<TSource, TProperty>(provider, property);
        }

        public static IObjectValidations<TSource> Validations<TSource>(
            this IAttributes<TSource> provider)
        {
            return new ObjectValidations<TSource>(provider);
        }
    }
}
