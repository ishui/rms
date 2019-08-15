namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class GenericTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return new InstanceDescriptor(val.GetType().GetConstructor(Type.EmptyTypes), null, false);
            }
            return base.ConvertTo(context, culture, val, destinationType);
        }
    }
}

