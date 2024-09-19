using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechConnect.WebUI.Services.Concrete;
using TechConnect.WebUI.Services.Interfaces;
using TechConnect.WebUI.Settings;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7237")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.AddDistributedMemoryCache(); // Bellek içi önbellek
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum zaman aþýmý
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});








// JWT doðrulama yapýlandýrmasý
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["ServiceApiSettings:IdentityServerUrl"];
    options.Audience = builder.Configuration["Jwt:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
})

.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Giriþ-Yap/";
    options.LogoutPath = "/Çýkýþ-Yap/";
    options.AccessDeniedPath = "/Pages/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = "TechConnectJwt";
});






builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();




builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));


builder.Services.ConfigureApplicationCookie(options => //çerezleri yapýlandýrdýk
{
    options.AccessDeniedPath = new PathString("/erisim-kisiti/");
    options.LoginPath = new PathString("/erisim-kisiti/");
});










var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowSpecificOrigins");



app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    var statusCode = response.StatusCode;

    if (statusCode == 401)
    {
        response.Redirect("/erisim-kisiti/");
    }
    else if (statusCode == 403)
    {
        response.Redirect("/erisim-kisiti/");
    }
});

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Homee}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.Run();
