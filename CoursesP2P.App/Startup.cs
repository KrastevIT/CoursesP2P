using AutoMapper;
using CloudinaryDotNet;
using CoursesP2P.App.Areas.Identity.Services;
using CoursesP2P.App.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Services.Students;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoursesP2P.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoursesP2PDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CoursesP2PDbContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddAutoMapper(typeof(Startup));

            RegisterServiceLayer(services);

            services.Configure<SendGridOptions>(this.Configuration.GetSection("EmailSettings"));

            Account account = new Account(
                         this.Configuration["Cloudinary:AppName"],
                         this.Configuration["Cloudinary:AppKey"],
                         this.Configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.SeedDatabase();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, SendGridEmailSender>();

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IInstructorsService, InstructorsService>();
            services.AddScoped<ILecturesService, LecturesService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
        }
    }
}
