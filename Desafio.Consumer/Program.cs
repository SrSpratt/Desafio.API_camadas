using Desafio.Consumer.Services;
using Desafio.Consumer.Services.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<EndpointGetter>();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
    ).AddCookie(cookie =>
    {
        cookie.LoginPath = "/Home/Index";
        cookie.LogoutPath = "/Home/Logout";
        cookie.AccessDeniedPath = "/Home/Index";
    }
);
builder.Services.AddScoped<AuthenticationMVC>();
builder.Services.AddControllersWithViews(
    options =>
    {
        options.Filters.Add<MVCErrorFilter>();
        //options.Filters.Add<SetTempModelStateAttribute>();
        //options.Filters.Add<RestoreTempModelStateAttribute>();
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var enUs = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new List<CultureInfo> { enUs },
    SupportedUICultures = new List<CultureInfo> { enUs }
};
app.UseRequestLocalization(localizationOptions);



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(
    new CookiePolicyOptions
    { 
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always
    }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
