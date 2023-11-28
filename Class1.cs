using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-create-a-new-method-for-an-enumeration
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

namespace inputreader {

}
