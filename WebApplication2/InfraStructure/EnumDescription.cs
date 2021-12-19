using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.InfraStructure
{
    public static class EnumDescription
    {
        public static IEnumerable<string> GetDescriptions(Type type)
        {
            var descs = new List<string>();
            var names = Enum.GetNames(type);
            foreach (var item in names)
            {
                var field = type.GetField(item);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute x in fds)
                {
                    descs.Add(x.Description);
                }

            }
            return descs;
        }
    }
}
