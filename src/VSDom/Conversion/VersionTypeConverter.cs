using System;
using System.ComponentModel;
using System.Globalization;

namespace VSDom.Conversion
{
    /// <summary>A <see cref="TypeConverter"/> that can convert <see cref="Version"/>.</summary>
    public class VersionTypeConverter : TypeConverter
    {
        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(Version) || base.CanConvertTo(context, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(value is string str && destinationType == typeof(Version))
            {
                Version.Parse(str);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (value is null || str != null)
            {
                return Version.Parse(str);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
