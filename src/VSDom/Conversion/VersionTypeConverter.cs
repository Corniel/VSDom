using System;
using System.ComponentModel;
using System.Globalization;

namespace VSDom.Conversion
{
    public class VersionTypeConverter : TypeConverter
    {
        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (value is null || str != null)
            {
                return FromString(str, culture);
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>Converts from <see cref="string"/>.</summary>
        protected Version FromString(string str, CultureInfo culture) => Version.Parse(str);
    }
}
