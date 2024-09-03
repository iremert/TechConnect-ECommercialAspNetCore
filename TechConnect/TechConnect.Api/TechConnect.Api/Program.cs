using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;
using TechConnect.BLL.Abstract;
using TechConnect.BLL.Concrete;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.DAL.MongoDbDriver;

var builder = WebApplication.CreateBuilder(args);







builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "Resource";
    opt.RequireHttpsMetadata = true;
});









// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutDal, MongoDbAboutRepository>();

builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, MongoDbTestimonialRepository>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, MongoDbCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, MongoDbProductRepository>();

builder.Services.AddScoped<IColorService, ColorManager>();
builder.Services.AddScoped<IColorDal, MongoDbColorRepository>();

builder.Services.AddScoped<ITagService, TagManager>();
builder.Services.AddScoped<ITagDal, MongoDbTagRepository>();

builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, MongoDbContactRepository>();

builder.Services.AddScoped<IContact2Service, Contact2Manager>();
builder.Services.AddScoped<IContact2Dal, MongoDbContact2Repository>();

builder.Services.AddScoped<IFavouriteService, FavouriteManager>();
builder.Services.AddScoped<IFavouriteDal, MongoDbFavouriteRepository>();

builder.Services.AddScoped<ICompareService, CompareManager>();
builder.Services.AddScoped<ICompareDal, MongoDbCompareRepository>();

builder.Services.AddScoped<IDiscountService, DiscountManager>();
builder.Services.AddScoped<IDiscountDal, MongoDbDiscountRepository>();




builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();










var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
