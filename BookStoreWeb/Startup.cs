using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options =>
                options.UseMySql(
                    "Server=127.0.0.1;port=3306;Database=BookStore_f;uid=root;pwd=Zhoujj@123!;Character Set=utf8;"));

            services.AddControllersWithViews();
            services.AddRouting(options => { options.LowercaseUrls = true; });
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //  disable  client side Validation 
            //     .AddViewOptions(option =>
            // {
            //     option.HtmlHelperOptions.ClientValidationEnabled = false;
            // });

#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
        }

    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
              //  endpoints.MapControllers(); //without specifying any routes.
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "bookstore/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}