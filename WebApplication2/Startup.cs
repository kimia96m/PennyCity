using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Models;
using WebApplication2.Models.Banners;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Carts;
using WebApplication2.Models.Products.Comments;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.KeyPoints;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Ratings;
using WebApplication2.Models.Products.SpecialProduct;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.Products.Tags;
using WebApplication2.Models.Profiles;
using WebApplication2.Models.Sellers;
using WebApplication2.Repository.EF;

namespace WebApplication2
{
    public class Startup
    {

        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration configuration { get; set; }
        public Startup(IConfiguration Configuration) { this.configuration = Configuration; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProducRepository, ProductRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IKeyPointRepository, keyPointRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagValuesRepository, TagValuesRepository>();
            services.AddScoped<ISpecificationRepository, SpecificationRepository>();
            services.AddScoped<ISpecificationGroupsRepository, SpecificationGroupsRepository>();
            services.AddScoped<ISpecificationValuesRepository, SpecificationValuesRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<ISpecialProductsRepository, SpecialProductsRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddMvc();  
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(configuration["Data:digikala:ConnectionString"]));
            services.AddIdentity<Operator, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(configuration["Data:digikala:ConnectionString"]);

            });            services.ConfigureApplicationCookie(options => options.LoginPath = "/Admin/Account/SignIn");            

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "MyArea",
                    template: "{area=exists}/{controller=Home}/{action=Index}/{id?}");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=Index}/{id?}");


                //{ area = exists}/
                //ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, configuration).Wait();

            });

        }

    }
}
//services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequiredLength = 6;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireNonAlphanumeric = false;
//});
//services.BuildServiceProvider(new ServiceProviderOptions
//{
//    ValidateScopes = false
//});            
//services.BuildServiceProvider(false);