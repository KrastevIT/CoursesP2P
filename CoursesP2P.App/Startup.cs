using Azure.Storage.Blobs;
using CoursesP2P.App.Areas.Identity.Services;
using CoursesP2P.App.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.AzureMedia;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Services.Payments;
using CoursesP2P.Services.ReCaptcha;
using CoursesP2P.Services.Reviews;
using CoursesP2P.Services.Students;
using CoursesP2P.ViewModels;
using CoursesP2P.ViewModels.AzureMedia;
using CoursesP2P.ViewModels.PayPal;
using CoursesP2P.ViewModels.ReCaptcha;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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
            services.Configure<ReCAPTCHASettings>(Configuration.GetSection("GooglereCAPTCHA"));

            services.AddDbContext<CoursesP2PDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CoursesP2PDbContext>();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            RegisterServiceLayer(services);

            services.Configure<SendGridOptions>(this.Configuration.GetSection("EmailSettings"));

            services.Configure<AzureMediaSettings>(this.Configuration.GetSection("AzureMedia"));

            BlobServiceClient blobServiceClient = new BlobServiceClient(this.Configuration["AzureBlobStorage:ConnectionString"]);
            services.AddSingleton(blobServiceClient);

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;
            });


            services.Configure<PayPalSettings>(Configuration.GetSection("PayPal"));

            services.AddMvc();
            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

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
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.SeedAdmin();

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>()
                    .MaxRequestBodySize = null;

                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                            "coursesCategory",
                            "Courses/Category",
                            new { controller = "Courses", action = "Category" });

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
            services.AddScoped<IReviewService, ReviewService>();
            services.AddTransient<IReCAPTCHAService, ReCAPTCHAService>();
            services.AddTransient<IPaymentsService, PaymentsService>();
            services.AddScoped<IAzureStorageBlobService, AzureStorageBlobService>();
            services.AddTransient<IAzureStorageBlob, AzureMediaService>();
        }
    }
}
