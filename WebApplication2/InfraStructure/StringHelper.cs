using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.InfraStructure
{
    public static class StringHelper
    {
        public static bool stringisnull(this string text)
        {
            if(string.IsNullOrEmpty(text)|| string.IsNullOrWhiteSpace(text)){
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
