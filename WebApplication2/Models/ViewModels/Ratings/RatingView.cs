using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModels.Ratings
{
    public class RatingView
    {
        public int Id { get; set; }
        public int? ProductItemId { get; set; }
        public int? SellerId { get; set; }
        public int Rate { get; set; }
        public int TotalRate { get; set; }
    }
}
