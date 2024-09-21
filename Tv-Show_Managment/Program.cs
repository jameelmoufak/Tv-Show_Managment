using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using TV.Domain.Models;
using TV.Infrastructure;
using TV.Infrastructure.Repositories;
using Tv_Show_Managment.Constraints;
using Tv_Show_Managment.Filters;
using Tv_Show_Managment.Repositories;
using Tv_Show_Managment.Transformers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["ValidateSlug"] = typeof(SlugConstraint);
    options.ConstraintMap["SlugTransformer"] = typeof(SlugParameterTransformer);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.SlidingExpiration = true;
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<TimerFilter>();
}).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
  .AddDataAnnotationsLocalization();
builder.Services.AddTransient<TimerFilter>();
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var SupportedLanguages = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-SY")
    };
    option.SupportedCultures = SupportedLanguages;
    option.SupportedUICultures = SupportedLanguages;
    option.DefaultRequestCulture = new RequestCulture("ar-SY");
    option.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

builder.Services.AddScoped<IRepository<Languages>, GenericRepository<Languages>>();
builder.Services.AddScoped<ITVShowRepository, TVShowRepository>();
builder.Services.AddDbContext<TVDbContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IStateRepository, SessionStateRepository>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
var options = app.Services.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TVDbContext>();
    TVDbContext.CreatingInitialTestingDataBase(context);
}
app.Run();
