using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace VSDom.Conversion
{
    internal static class TypeConverters
    {
        public static TypeConverter Get(Type classType, string propertyName)
        {
            Guard.NotNull(classType, nameof(classType));

            var property = classType.GetProperty(propertyName);

            if (propertyStore.TryGetValue(property, out TypeConverter converter))
            {
                return converter;
            }

            lock (locker)
            {
                if (!propertyStore.TryGetValue(property, out converter))
                {
                    var attr = property.GetCustomAttribute<TypeConverterAttribute>(true);

                    if(attr != null)
                    {
                        converter = (TypeConverter)Activator.CreateInstance(attr.GetType());
                    }
                    else if(!typeStore.TryGetValue(property.PropertyType, out converter))
                    {
                        converter = TypeDescriptor.GetConverter(property.GetType());
                        typeStore[property.PropertyType] = converter;
                    }
                    propertyStore[property] = converter;
                }
            }
            return converter;
        }

        private static readonly Dictionary<PropertyInfo, TypeConverter> propertyStore = new Dictionary<PropertyInfo, TypeConverter>();
        private static readonly Dictionary<Type, TypeConverter> typeStore = new Dictionary<Type, TypeConverter>
        {
            { typeof(Version), new VersionTypeConverter() },
        };

        private static readonly object locker = new object();
    }
}
