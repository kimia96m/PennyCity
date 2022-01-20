using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Products;

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
        public static string GetPaymentDescriptions(PaymentType type)
        {
            var descript = type.GetType().GetField(type.ToString());
            var desti = descript.GetCustomAttributes(typeof(DescriptionAttribute), true);
            var description = type.ToString();
            if (desti != null && desti.Length > 0)
            {
                description = ((DescriptionAttribute)desti[0]).Description;
            }


            return description;
        }
        public static string GetStateDescriptions(OrderState type)
        {
            var descript = type.GetType().GetField(type.ToString());
            var desti = descript.GetCustomAttributes(typeof(DescriptionAttribute), true);
            var description = type.ToString();
            if (desti != null && desti.Length > 0)
            {
                description = ((DescriptionAttribute)desti[0]).Description;
            }


            return description;
        }
    }
}
