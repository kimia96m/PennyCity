using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.InfraStructure
{
    public class RatingCalculator
    {
        public int Ratings(int rate, int count)
        {
            int totalrate = rate / count;
            return totalrate;
        }
    }
}
