namespace VeterinaryClinic.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Pioneer.Pagination;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Common;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Data.Seeding;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;

    // using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Services.Messaging;
    using VeterinaryClinic.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton(this.configuration);
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<INewsScraperService, NewsScraperService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IServiceScraperService, ServiceScraperService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IPaginatedMetaService, PaginatedMetaService>();
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IOwnersService, OwnersService>();
            services.AddTransient<IPetsService, PetsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IVetsService, VetsService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddTransient<IPetsMedicationsService, PetsMedicationsService>();

            Account account = new Account(
                this.configuration["Cloudinary:AppName"],
                this.configuration["Cloudinary:AppKey"],
                this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration["Facebook:AppID"];
                facebookOptions.AppSecret = this.configuration["Facebook:AppSecret"];
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = this.configuration["GoogleSignIn:ClientID"];
                googleOptions.ClientSecret = this.configuration["GoogleSignIn:ClientAppSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("Home/StatusCodeError?errorCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
