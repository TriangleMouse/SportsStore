using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace SportsStore
{
    public class Startup
    {

        public Startup(IHostEnvironment hostEnv) => this.Configuration= new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProductRepository,EFProductRepository> ();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp=>SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();// будем хранить состояние сеанса в памяти(до перезапуска проги)
            services.AddSession();
           // services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            /*app.UseMvc(routes=>
            {
                routes.MapRoute(
                        name: "default",
                        template: "{controller=Product}/{action=List}/{id?}"
                    );
            });*/

            app.UseEndpoints(
                endpoints =>{

                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "{category}/Page{productPage:int}",
                           defaults: new { controller = "Product", action = "List" });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "Page{productPage:int}",
                           defaults: new { controller = "Product", action = "List", productPage = 1 });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "{category}",
                           defaults: new {controller= "Product", action= "List", productPage=1 });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "",
                           defaults: new { controller = "Product", action = "List", productPage = 1 });
                    endpoints.MapControllerRoute(
                        name: null,
                        pattern: "{controller}/{action}/{id?}");
                });

            SeedData.EnsurePopulated(app);
        }
    }
}
