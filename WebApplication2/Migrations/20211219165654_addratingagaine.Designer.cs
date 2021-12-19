﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Models;

namespace WebApplication2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211219165654_addratingagaine")]
    partial class addratingagaine
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApplication2.Models.Banners.Banner", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ext");

                    b.Property<string>("imgagesrc");

                    b.Property<bool?>("ispecial");

                    b.Property<string>("link");

                    b.HasKey("id");

                    b.ToTable("banners");
                });

            modelBuilder.Entity("WebApplication2.Models.Operator", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("lastname");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApplication2.Models.Orders.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address");

                    b.Property<string>("customerId");

                    b.Property<string>("fishnumber");

                    b.Property<DateTime>("orderdate");

                    b.Property<byte>("paymenttypes");

                    b.Property<DateTime?>("registrationdate");

                    b.Property<byte>("shippingtypes");

                    b.Property<byte>("state");

                    b.Property<double>("totalprice");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("WebApplication2.Models.Orders.OrderItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("orderid");

                    b.Property<double>("price");

                    b.Property<int?>("productitemid");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.HasIndex("orderid");

                    b.HasIndex("productitemid");

                    b.ToTable("orderitems");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Brands.Brand", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("State");

                    b.Property<DateTime>("createdatetime");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<string>("slug");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Carts.Cart", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("customerId");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.ToTable("carts");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Carts.CartItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("cartid");

                    b.Property<double>("price");

                    b.Property<int?>("productitemid");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.HasIndex("cartid");

                    b.HasIndex("productitemid");

                    b.ToTable("cartitem");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Comments.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<int>("productid");

                    b.Property<string>("text");

                    b.Property<string>("userId");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Groups.BannerGroups", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdatetime");

                    b.Property<string>("creatorId");

                    b.Property<string>("ext");

                    b.Property<int>("groupnumber");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("bannergroup");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Groups.Group", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("State");

                    b.Property<DateTime>("createdatetime");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<string>("slug");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("group");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.KeyPoints.KeyPoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("creatdatetime");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<int?>("productId");

                    b.Property<string>("title");

                    b.Property<int>("types");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.HasIndex("productId");

                    b.ToTable("keypoint");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Brandid");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<int>("Groupid");

                    b.Property<string>("LastModifierId");

                    b.Property<DateTime?>("LastModifyDate");

                    b.Property<string>("PrimaryTitle");

                    b.Property<string>("SecondaryTitle");

                    b.Property<byte?>("state");

                    b.HasKey("Id");

                    b.HasIndex("Brandid");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Groupid");

                    b.HasIndex("LastModifierId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.ProductItems.ItemTagValue", b =>
                {
                    b.Property<int>("tagvalueid");

                    b.Property<int>("productitemid");

                    b.HasKey("tagvalueid", "productitemid");

                    b.HasIndex("productitemid");

                    b.ToTable("itemtagvalues");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.ProductItems.ProductItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<double?>("discount");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<double>("price");

                    b.Property<int?>("productId");

                    b.Property<byte>("quantity");

                    b.Property<int>("sellerid");

                    b.Property<byte?>("state");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.HasIndex("productId");

                    b.HasIndex("sellerid");

                    b.ToTable("productitems");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Ratings.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId");

                    b.Property<int?>("ProductItemId");

                    b.Property<int>("Rate");

                    b.Property<int?>("SellerId");

                    b.Property<string>("customer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId")
                        .IsUnique()
                        .HasFilter("[SellerId] IS NOT NULL");

                    b.ToTable("ratings");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.SpecialProduct.SpecialProducts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("brandid");

                    b.Property<int?>("discount");

                    b.Property<TimeSpan?>("leftedtime");

                    b.Property<int>("pnumb");

                    b.Property<double>("price");

                    b.Property<byte?>("state");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("brandid");

                    b.ToTable("specialprodcut");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.Specification", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<int?>("specificationgroupsid");

                    b.Property<byte>("state");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.HasIndex("specificationgroupsid");

                    b.ToTable("specifications");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.SpecificationGroups", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<int?>("groupid");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<byte>("state");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("groupid");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("specificationgroups");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.SpecificationValues", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<int?>("productId");

                    b.Property<int?>("specificationid");

                    b.Property<byte>("state");

                    b.Property<string>("valuetitle");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.HasIndex("productId");

                    b.HasIndex("specificationid");

                    b.ToTable("specificationvalues");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Tags.Tag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<byte?>("state");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Tags.TagValeus", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<byte?>("state");

                    b.Property<int?>("tagsid");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.HasIndex("tagsid");

                    b.ToTable("tagvalues");
                });

            modelBuilder.Entity("WebApplication2.Models.Profiles.Address", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("city");

                    b.Property<string>("customerId");

                    b.Property<string>("location");

                    b.Property<string>("province");

                    b.Property<string>("tel");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("WebApplication2.Models.Sellers.Seller", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdate");

                    b.Property<string>("creatorId");

                    b.Property<string>("description");

                    b.Property<string>("lastmodifierId");

                    b.Property<DateTime?>("lastmodifydate");

                    b.Property<double?>("paid");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("lastmodifierId");

                    b.ToTable("sellers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication2.Models.Operator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication2.Models.Orders.Order", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");
                });

            modelBuilder.Entity("WebApplication2.Models.Orders.OrderItem", b =>
                {
                    b.HasOne("WebApplication2.Models.Orders.Order", "order")
                        .WithMany("orderitems")
                        .HasForeignKey("orderid");

                    b.HasOne("WebApplication2.Models.Products.ProductItems.ProductItem", "productitem")
                        .WithMany()
                        .HasForeignKey("productitemid");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Brands.Brand", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Carts.Cart", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Carts.CartItem", b =>
                {
                    b.HasOne("WebApplication2.Models.Products.Carts.Cart", "cart")
                        .WithMany("cartitem")
                        .HasForeignKey("cartid");

                    b.HasOne("WebApplication2.Models.Products.ProductItems.ProductItem", "productitem")
                        .WithMany()
                        .HasForeignKey("productitemid");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Comments.Comment", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Groups.BannerGroups", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Groups.Group", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.KeyPoints.KeyPoint", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");

                    b.HasOne("WebApplication2.Models.Products.Product", "product")
                        .WithMany("Keypoints")
                        .HasForeignKey("productId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Product", b =>
                {
                    b.HasOne("WebApplication2.Models.Products.Brands.Brand", "Brands")
                        .WithMany()
                        .HasForeignKey("Brandid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication2.Models.Operator", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("WebApplication2.Models.Products.Groups.Group", "Groups")
                        .WithMany()
                        .HasForeignKey("Groupid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication2.Models.Operator", "LastModifier")
                        .WithMany()
                        .HasForeignKey("LastModifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.ProductItems.ItemTagValue", b =>
                {
                    b.HasOne("WebApplication2.Models.Products.ProductItems.ProductItem", "productitem")
                        .WithMany("itemtagvalue")
                        .HasForeignKey("productitemid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication2.Models.Products.Tags.TagValeus", "tagvalues")
                        .WithMany("itemtagvalue")
                        .HasForeignKey("tagvalueid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication2.Models.Products.ProductItems.ProductItem", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");

                    b.HasOne("WebApplication2.Models.Products.Product", "product")
                        .WithMany("productitem")
                        .HasForeignKey("productId");

                    b.HasOne("WebApplication2.Models.Sellers.Seller")
                        .WithMany("sold")
                        .HasForeignKey("sellerid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Ratings.Rating", b =>
                {
                    b.HasOne("WebApplication2.Models.Products.Product")
                        .WithMany("ratings")
                        .HasForeignKey("ProductId");

                    b.HasOne("WebApplication2.Models.Sellers.Seller")
                        .WithOne("rating")
                        .HasForeignKey("WebApplication2.Models.Products.Ratings.Rating", "SellerId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.SpecialProduct.SpecialProducts", b =>
                {
                    b.HasOne("WebApplication2.Models.Products.Brands.Brand", "brand")
                        .WithMany()
                        .HasForeignKey("brandid");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.Specification", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");

                    b.HasOne("WebApplication2.Models.Products.Specification.SpecificationGroups", "specificationgroups")
                        .WithMany("specification")
                        .HasForeignKey("specificationgroupsid");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.SpecificationGroups", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Products.Groups.Group", "group")
                        .WithMany("specificationgroups")
                        .HasForeignKey("groupid");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Specification.SpecificationValues", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");

                    b.HasOne("WebApplication2.Models.Products.Product", "product")
                        .WithMany("specificationvalues")
                        .HasForeignKey("productId");

                    b.HasOne("WebApplication2.Models.Products.Specification.Specification", "specification")
                        .WithMany("specificationvalues")
                        .HasForeignKey("specificationid");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Tags.Tag", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });

            modelBuilder.Entity("WebApplication2.Models.Products.Tags.TagValeus", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");

                    b.HasOne("WebApplication2.Models.Products.Tags.Tag", "tags")
                        .WithMany("tagValues")
                        .HasForeignKey("tagsid");
                });

            modelBuilder.Entity("WebApplication2.Models.Profiles.Address", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "customer")
                        .WithMany("address")
                        .HasForeignKey("customerId");
                });

            modelBuilder.Entity("WebApplication2.Models.Sellers.Seller", b =>
                {
                    b.HasOne("WebApplication2.Models.Operator", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("WebApplication2.Models.Operator", "lastmodifier")
                        .WithMany()
                        .HasForeignKey("lastmodifierId");
                });
#pragma warning restore 612, 618
        }
    }
}
