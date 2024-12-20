using E_commerceSite.Web.Application.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Website.Data.Models;
using Website.Infrastructure.Extensions;
using Website.Services.Data.Interfaces;
using Website.Services.Data;
using Website.Data.Repository.Interfaces;
using Website.Data.Repository;

namespace E_commerceSite.Web.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!; //admin@web.com
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username")!;//admin@web.com
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!; //Admin123

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {
                    ConfigureIdentity(builder, cfg);
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>();

            //Services
            //builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            //builder.Services.RegisterRepositories(typeof(CartProducts).Assembly);

            //builder.Services.RegisterUserDefinedServices(typeof(IUserService).Assembly);
            //builder.Services.RegisterUserDefinedServices(typeof(IOrderService).Assembly);
            //builder.Services.RegisterUserDefinedServices(typeof(IHomeService).Assembly);
            //builder.Services.AddScoped<IHomeService, HomeService>();
            //builder.Services.AddScoped<IRepository<CartProducts, Guid>, BaseRepository<CartProducts, Guid>>();


            //// Register repositories
            builder.Services.AddScoped<IRepository<CartProducts, Guid>, BaseRepository<CartProducts, Guid>>();
            builder.Services.AddScoped<IRepository<Product, Guid>, BaseRepository<Product, Guid>>();
            builder.Services.AddScoped<IRepository<Order, Guid>, BaseRepository<Order, Guid>>();

            builder.Services.RegisterUserDefinedServices(typeof(IUserService).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IOrderService).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IHomeService).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IProductService).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IBaseService).Assembly);

            //// Register services
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IOrderService, OrderService>();
            //builder.Services.AddScoped<IHomeService, HomeService>();

            //builder.Services.RegisterRepositories(typeof(BaseRepository<CartProducts, Guid>).Assembly);
            //builder.Services.RegisterRepositories(typeof(BaseRepository<Product, Guid>).Assembly);
            //builder.Services.RegisterRepositories(typeof(BaseRepository<Order, Guid>).Assembly);

            //builder.Services.AddApplicationServices();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.SeedRoles();
            app.SeedAdministrator(adminEmail, adminUsername, adminPassword);

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "Errors",
                pattern: "{controller=Home}/{action=Index}/{statusCode?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.ApplyMigrations();

            app.Run();
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
        {
            cfg.Password.RequireDigit =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            cfg.Password.RequireUppercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            cfg.Password.RequireNonAlphanumeric =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            cfg.Password.RequiredUniqueChars =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail =
                builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }

    }
}
