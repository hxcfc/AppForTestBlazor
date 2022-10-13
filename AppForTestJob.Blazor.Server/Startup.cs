using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using AppForTestJob.Blazor.Server.Services;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.WebApi.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;

namespace AppForTestJob.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => {
            builder.UseApplication<AppForTestJobBlazorApplication>();
            builder.Modules
                .AddValidation(options => {
                    options.AllowValidationDetailsAccess = false;
                })
                .Add<AppForTestJob.Module.AppForTestJobModule>()
            	.Add<AppForTestJobBlazorModule>();
            builder.ObjectSpaceProviders
                .AddEFCore().WithDbContext<AppForTestJob.Module.BusinessObjects.AppForTestJobEFCoreDbContext>((serviceProvider, options) => {
                    // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                    // Do not use this code in production environment to avoid data loss.
                    // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                    //options.UseInMemoryDatabase("InMemory");
                    string connectionString = null;
                    if(Configuration.GetConnectionString("ConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("ConnectionString");
                    }
#if EASYTEST
                    if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                    }
#endif
                    ArgumentNullException.ThrowIfNull(connectionString);
                    options.UseSqlServer(connectionString);
                    options.UseLazyLoadingProxies();
                })
                .AddNonPersistent();
        });
        services.AddXafWebApi(Configuration, options =>
        {
            // Use options.BusinessObject<YourBusinessObject>() 
            // to make the Business Object available in the Web API and
            // generate the GET, POST, PUT, and DELETE HTTP methods for it.
        });
    // in XPO applications, uncomment the following line
    // .AddXpoServices(); 
    services.AddControllers().AddOData((options, serviceProvider) => {
        options
            .AddRouteComponents("api/odata", new EdmModelBuilder(serviceProvider).GetEdmModel())
            .EnableQueryFeatures(100);
    });
        services.AddSwaggerGen(c => {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MySolution API",
                Version = "v1",
                Description = @"Use AddXafWebApi(Configuration, options) in the MySolution.Blazor.Server\Startup.cs file to make Business Objects available in the Web API."
            });
        });
        services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySolution WebApi v1");
            });
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}
