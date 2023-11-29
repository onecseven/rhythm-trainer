using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumExtension
{
    public static class Extensions
    {
        public static string Name<T>(this T key)
        {
            return Enum.GetName(typeof(T), key);
        }
    }
}
