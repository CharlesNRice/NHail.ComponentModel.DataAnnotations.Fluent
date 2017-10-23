using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class AttributeProvider<TSource> : TypeDescriptionProvider
    {
        private readonly IList<Attribute> _validations = new List<Attribute>();

        private readonly IList<KeyValuePair<string, Attribute>> _propValidations =
            new List<KeyValuePair<string, Attribute>>();

        public AttributeProvider() : base(TypeDescriptor.GetProvider(typeof(TSource)))
        {
            TypeDescriptor.AddProvider(this, typeof(TSource));
        }

        public void AddAttribute(Attribute validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }
            _validations.Add(validation);
        }

        public void AddPropertyAttribute(string property, Attribute validation)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }
            _propValidations.Add(new KeyValuePair<string, Attribute>(property, validation));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new AttributeTypeDescriptor(_validations, _propValidations,
                base.GetTypeDescriptor(objectType, instance));
        }
    }
}
