using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.KeyPoints;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.Products.Tags;
using WebApplication2.Models.Products.Carts;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Profiles;
using WebApplication2.Models.Banners;
using WebApplication2.Models.Products.SpecialProduct;
using WebApplication2.Models.Products.Comments;
using WebApplication2.Models.Products.Ratings;
using WebApplication2.Models.Sellers;

namespace WebApplication2.Models
{
    public class ApplicationDbContext : IdentityDbContext<Operator>
    {
        //ModelBuilder.Entity<Capture>().Property(p=>p.Capture).HasCoversion(p=>p.Value,p=>Capture.FromValue(p));

        public DbSet<Products.Product> product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Group> group { get; set; }
        public DbSet<SpecificationGroups> specificationgroups { get; set; }
        public DbSet<Specification> specifications { get; set; }
        public DbSet<SpecificationValues> specificationvalues { get; set; }
        public DbSet<KeyPoint> keypoint { get; set; }
        //public DbSet<KeyPointsTypes> keypointtype { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<TagValeus> tagvalues { get; set; }
        public DbSet<ProductItem> productitems { get; set; }
        public DbSet<ItemTagValue> itemtagvalues { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartitem { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<SpecialProducts> specialprodcut { get; set; }
        public DbSet<OrderItem> orderitems { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Banner> banners { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Rating> ratings { get; set; }
        public DbSet<Seller> sellers { get; set; }
        public DbSet<BannerGroups> bannergroup { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public static async Task CreateAdminAccount(IServiceProvider ServiceProvider, IConfiguration configuration)
        {

            using (var servicesscop = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<Operator> userManager = servicesscop.ServiceProvider.GetService<UserManager<Operator>>();
                RoleManager<IdentityRole> roleManager = servicesscop.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                string username = configuration["Data:AdminUser:Name"];
                string email = configuration["Data: AdminUser:Email"];
                string password = configuration["Data:AdminUser:Password"];
                string role = configuration["Data:AdminUser:Role"];
                if (await userManager.FindByNameAsync(username) == null)
                {
                    if (await userManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    Operator user = new Operator
                    {
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    };

                }




            }


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ItemTagValue>().HasKey(sc => new { sc.tagvalueid, sc.productitemid });
            builder.Entity<ItemTagValue>()
           .HasOne<TagValeus>(sc => sc.tagvalues)
            .WithMany(s => s.itemtagvalue)
             .HasForeignKey(sc => sc.tagvalueid);


            builder.Entity<ItemTagValue>()
                .HasOne<ProductItem>(sc => sc.productitem)
                .WithMany(s => s.itemtagvalue)
                .HasForeignKey(sc => sc.productitemid);
            base.OnModelCreating(builder);

           // builder.Entity<Operator>()
           //.HasOne<Address>(s => s.address)
           //.WithOne(ad => ad.customer)
           //.HasForeignKey<Address>(ads => ads.addressofuserid);
        }
    }
  
}


