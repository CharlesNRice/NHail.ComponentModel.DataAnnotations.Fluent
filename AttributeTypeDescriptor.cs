using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHail.ComponentModel.DataAnnotations.Fluent
{
    public class AttributeTypeDescriptor : CustomTypeDescriptor
    {
        private readonly HashSet<Attribute> _attributes;
        private readonly ILookup<string, Attribute> _propertyAttributes;
        private readonly PropertyDescriptorCollection _propertyDescriptorCollection;

        public AttributeTypeDescriptor(IEnumerable<Attribute> attributes,
            IEnumerable<KeyValuePair<string, Attribute>> propertyAttributes,
            ICustomTypeDescriptor parent) : base(parent)
        {
            _attributes = new HashSet<Attribute>(attributes);
            _propertyAttributes =
                propertyAttributes.ToLookup(kv => kv.Key, kv => kv.Value);

            if (_propertyAttributes.Any())
            {
                _propertyDescriptorCollection =
                    new PropertyDescriptorCollection(CreateProperties(parent.GetProperties()).ToArray());
            }
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return _propertyDescriptorCollection ?? base.GetProperties();
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _propertyDescriptorCollection ?? base.GetProperties(attributes);
        }

        private IEnumerable<PropertyDescriptor> CreateProperties(PropertyDescriptorCollection properties)
        {
            foreach (PropertyDescriptor property in properties)
            {
                if (!_propertyAttributes.Contains(property.Name))
                {
                    yield return property;
                }
                else
                {
                    yield return TypeDescriptor.CreateProperty(property.ComponentType, property,
                        property.Attributes.Cast<Attribute>().Concat(_propertyAttributes[property.Name]).ToArray());
                }
            }
        }

        public override object GetPropertyOwner(PropertyDescriptor pd)
        {
            var owner = base.GetPropertyOwner(pd);
            if (owner != null)
            {
                return owner;
            }

            if (_propertyAttributes.Contains(pd.Name))
            {
                return this;
            }
            return null;
        }

        public override AttributeCollection GetAttributes()
        {
            // Filter out any attributes that have the same TypeId
            //  using a hashset with an IEqualityComparer to do the work
            //  Add our attributes first then add any unique ones from the base code
            var attributes = new HashSet<Attribute>(_attributes,
                ProjectionEqualityComparer<Attribute>.Create(a => a.TypeId));
            foreach (Attribute attribute in base.GetAttributes())
            {
                attributes.Add(attribute);
            }
            return new AttributeCollection(attributes.ToArray());
        }
    }
}
