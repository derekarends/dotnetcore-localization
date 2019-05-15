using System.Globalization;
using Core.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLocalization();
      services.Configure<RequestLocalizationOptions>(o =>
      {
        var supportedCultures = new[]
        {
          new CultureInfo("en-US"),
          new CultureInfo("fr-FR")
        };

        // State what the default culture for your application is. This will be used if no specific culture
        // can be determined for a given request.
        o.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
        o.SupportedCultures = supportedCultures;
        o.SupportedUICultures = supportedCultures;

        // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
        // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
        // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
        o.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
      });
      
      services.AddMvc()
        .AddDataAnnotationsLocalization(options =>
        {
          options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResources));
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    { 
      var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(locOptions.Value);
      
      app.UseMvc();
    }
  }
}