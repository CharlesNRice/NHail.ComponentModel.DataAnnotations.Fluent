using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class ProjectionEqualityComparer<TSource>
    {
        /// <summary>
        /// Factory method to create the ProjectionEqualityComparer
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static IEqualityComparer<TSource> Create<TResult>(Func<TSource, TResult> projection)
        {
            return new ProjectionEqualityComparer<TSource, TResult>(projection);
        }
    }


    public class ProjectionEqualityComparer<TSource, TResult> : IEqualityComparer<TSource>
    {
        private readonly Func<TSource, TResult> _projection;

        public ProjectionEqualityComparer(Func<TSource, TResult> projection)
        {
            _projection = projection;
        }

        public virtual bool Equals(TSource x, TSource y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null)
            {
                return false;
            }
            if (y == null)
            {
                return false;
            }

            var xData = _projection(x);
            var yData = _projection(y);

            return EqualityComparer<TResult>.Default.Equals(xData, yData);
        }

        public virtual int GetHashCode(TSource obj)
        {
            var objData = _projection(obj);

            return EqualityComparer<TResult>.Default.GetHashCode(objData);
        }
    }
}
