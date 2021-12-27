using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addallagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "banners",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    link = table.Column<string>(nullable: true),
                    imgagesrc = table.Column<string>(nullable: true),
                    ext = table.Column<string>(nullable: true),
                    ispecial = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    province = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    tel = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    customerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_addresses_AspNetUsers_customerId",
                        column: x => x.customerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bannergroup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    groupnumber = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    ext = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannergroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_bannergroup_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bannergroup_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    slug = table.Column<string>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.id);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    customerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_carts_AspNetUsers_customerId",
                        column: x => x.customerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    text = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    userId = table.Column<string>(nullable: true),
                    productid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    slug = table.Column<string>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_group_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    customerId = table.Column<string>(nullable: true),
                    orderdate = table.Column<DateTime>(nullable: false),
                    totalprice = table.Column<double>(nullable: false),
                    shippingtypes = table.Column<byte>(nullable: false),
                    paymenttypes = table.Column<byte>(nullable: false),
                    state = table.Column<byte>(nullable: false),
                    registrationdate = table.Column<DateTime>(nullable: true),
                    fishnumber = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_customerId",
                        column: x => x.customerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sellers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    paid = table.Column<double>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellers", x => x.id);
                    table.ForeignKey(
                        name: "FK_sellers_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellers_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tags_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specialprodcut",
                columns: table => new
                {
                    title = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pnumb = table.Column<int>(nullable: false),
                    brandid = table.Column<int>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    leftedtime = table.Column<TimeSpan>(nullable: true),
                    discount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialprodcut", x => x.id);
                    table.ForeignKey(
                        name: "FK_specialprodcut_Brand_brandid",
                        column: x => x.brandid,
                        principalTable: "Brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrimaryTitle = table.Column<string>(nullable: true),
                    SecondaryTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<string>(nullable: true),
                    LastModifyDate = table.Column<DateTime>(nullable: true),
                    Groupid = table.Column<int>(nullable: false),
                    Brandid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_Brand_Brandid",
                        column: x => x.Brandid,
                        principalTable: "Brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_group_Groupid",
                        column: x => x.Groupid,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specificationgroups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    groupid = table.Column<int>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specificationgroups", x => x.id);
                    table.ForeignKey(
                        name: "FK_specificationgroups_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationgroups_group_groupid",
                        column: x => x.groupid,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationgroups_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tagvalues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    tagsid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tagvalues", x => x.id);
                    table.ForeignKey(
                        name: "FK_tagvalues_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tagvalues_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tagvalues_tags_tagsid",
                        column: x => x.tagsid,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "keypoint",
                columns: table => new
                {
                    title = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    types = table.Column<int>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    creatdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    productId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keypoint", x => x.id);
                    table.ForeignKey(
                        name: "FK_keypoint_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_keypoint_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_keypoint_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productitems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productId = table.Column<int>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    discount = table.Column<double>(nullable: true),
                    quantity = table.Column<byte>(nullable: false),
                    state = table.Column<byte>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    sellerid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_productitems_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_sellers_sellerid",
                        column: x => x.sellerid,
                        principalTable: "sellers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductItemId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    customer = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ratings_sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "sellers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specifications",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    specificationgroupsid = table.Column<int>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_specifications_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specifications_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specifications_specificationgroups_specificationgroupsid",
                        column: x => x.specificationgroupsid,
                        principalTable: "specificationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cartitem",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cartid = table.Column<int>(nullable: true),
                    productitemid = table.Column<int>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartitem", x => x.id);
                    table.ForeignKey(
                        name: "FK_cartitem_carts_cartid",
                        column: x => x.cartid,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cartitem_productitems_productitemid",
                        column: x => x.productitemid,
                        principalTable: "productitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "itemtagvalues",
                columns: table => new
                {
                    tagvalueid = table.Column<int>(nullable: false),
                    productitemid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemtagvalues", x => new { x.tagvalueid, x.productitemid });
                    table.ForeignKey(
                        name: "FK_itemtagvalues_productitems_productitemid",
                        column: x => x.productitemid,
                        principalTable: "productitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemtagvalues_tagvalues_tagvalueid",
                        column: x => x.tagvalueid,
                        principalTable: "tagvalues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderid = table.Column<int>(nullable: true),
                    productitemid = table.Column<int>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_orderitems_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orderitems_productitems_productitemid",
                        column: x => x.productitemid,
                        principalTable: "productitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specificationvalues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    valuetitle = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    productId = table.Column<int>(nullable: true),
                    specificationid = table.Column<int>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specificationvalues", x => x.id);
                    table.ForeignKey(
                        name: "FK_specificationvalues_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_specifications_specificationid",
                        column: x => x.specificationid,
                        principalTable: "specifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_customerId",
                table: "addresses",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_bannergroup_creatorId",
                table: "bannergroup",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_bannergroup_lastmodifierId",
                table: "bannergroup",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_creatorId",
                table: "Brand",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_lastmodifierId",
                table: "Brand",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_cartid",
                table: "cartitem",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_productitemid",
                table: "cartitem",
                column: "productitemid");

            migrationBuilder.CreateIndex(
                name: "IX_carts_customerId",
                table: "carts",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_group_creatorId",
                table: "group",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_group_lastmodifierId",
                table: "group",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_itemtagvalues_productitemid",
                table: "itemtagvalues",
                column: "productitemid");

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_creatorId",
                table: "keypoint",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_lastmodifierId",
                table: "keypoint",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_productId",
                table: "keypoint",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_productitemid",
                table: "orderitems",
                column: "productitemid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customerId",
                table: "orders",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_product_Brandid",
                table: "product",
                column: "Brandid");

            migrationBuilder.CreateIndex(
                name: "IX_product_CreatorId",
                table: "product",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_Groupid",
                table: "product",
                column: "Groupid");

            migrationBuilder.CreateIndex(
                name: "IX_product_LastModifierId",
                table: "product",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_creatorId",
                table: "productitems",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_lastmodifierId",
                table: "productitems",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_productId",
                table: "productitems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_sellerid",
                table: "productitems",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_ProductId",
                table: "ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_SellerId",
                table: "ratings",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_creatorId",
                table: "sellers",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_lastmodifierId",
                table: "sellers",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specialprodcut_brandid",
                table: "specialprodcut",
                column: "brandid");

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_creatorId",
                table: "specificationgroups",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_groupid",
                table: "specificationgroups",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_lastmodifierId",
                table: "specificationgroups",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_creatorId",
                table: "specifications",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_lastmodifierId",
                table: "specifications",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_specificationgroupsid",
                table: "specifications",
                column: "specificationgroupsid");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_creatorId",
                table: "specificationvalues",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_lastmodifierId",
                table: "specificationvalues",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_productId",
                table: "specificationvalues",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_specificationid",
                table: "specificationvalues",
                column: "specificationid");

            migrationBuilder.CreateIndex(
                name: "IX_tags_creatorId",
                table: "tags",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_lastmodifierId",
                table: "tags",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_creatorId",
                table: "tagvalues",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_lastmodifierId",
                table: "tagvalues",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_tagsid",
                table: "tagvalues",
                column: "tagsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "bannergroup");

            migrationBuilder.DropTable(
                name: "banners");

            migrationBuilder.DropTable(
                name: "cartitem");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "itemtagvalues");

            migrationBuilder.DropTable(
                name: "keypoint");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "specialprodcut");

            migrationBuilder.DropTable(
                name: "specificationvalues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "tagvalues");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "productitems");

            migrationBuilder.DropTable(
                name: "specifications");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "sellers");

            migrationBuilder.DropTable(
                name: "specificationgroups");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
