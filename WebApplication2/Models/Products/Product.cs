using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.KeyPoints;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.Products.Ratings;
using WebApplication2.Models.Products.Comments;

namespace WebApplication2.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        public string Ext { get; set; }
        public Brand Brands { get; set; }
        public Group Groups { get; set; }
        public States? state { get; set; }
        public Operator Creator { get; set; }
        public DateTime CreatDate { get; set; }
        public Operator LastModifier { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public int Groupid { get; set; }
        public int Brandid { get; set; }
        public PaginatedList<Comment> comments {get;set;}
        public List<ProductItem> productitem { get; set; }
        public List<KeyPoint> Keypoints { get; set; }
        public List<SpecificationValues> specificationvalues { get; set; }
        public List<Rating> ratings { get; set; }
    }
}
