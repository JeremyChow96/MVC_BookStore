using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStoreWeb
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

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

            services.AddSingleton<IMessageRepository, MessageRepository>();

            //IOptions works on singleton

            services.Configure<NewBookAlertConfig>("InternalBook",_configuration.GetSection("NewBookAlert"));
            // alert: this will override the previous configuration 
            services.Configure<NewBookAlertConfig>("ThirdPartyBook",_configuration.GetSection("ThirdPartyBook"));


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
           
            app.UseAuthentication();

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