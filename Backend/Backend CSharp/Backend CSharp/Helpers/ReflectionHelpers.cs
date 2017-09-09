using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Helpers
{
    /// <summary>
    /// https://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection-in-c-sharp
    /// </summary>
    public static class ReflectionHelpers
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
    }
}
