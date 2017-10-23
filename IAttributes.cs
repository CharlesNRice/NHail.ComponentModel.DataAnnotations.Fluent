﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public interface IAttributes<TSource>
    {
        /// <summary>
        /// Adds the attributes to the property
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IAttributes<TSource> AddPropertyAttributes<TProperty>(Expression<Func<TSource, TProperty>> property,
            params Attribute[] attributes);

        /// <summary>
        /// Adds the attributes to the class
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IAttributes<TSource> AddAttributes(params Attribute[] attributes);
    }

}
