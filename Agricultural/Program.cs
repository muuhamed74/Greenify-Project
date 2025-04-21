using Agricultural.Repo.Data;
using Agricultural.Repo.Data.DataSeeding;
using Agricultural.Repo.Repositories;
using Agricultural.Serv.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Web;




//using DataAcess;
//using Microsoft.EntityFrameworkCore;
//using Scalar.AspNetCore;
//using Models.DTOs.Mapper;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNetCore.OpenApi;
//using Microsoft.OpenApi.Models;
//using DataAcess.Repos;
//using DataAcess.Repos.IRepos;
//using Models.Domain;
//using IdentityManagerAPI.Middlewares;
//using IdentityManager.Services.ControllerService.IControllerService;
//using IdentityManager.Services.ControllerService;

namespace Agricultural
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen();

            // Add database context
            builder.Services.AddDbContext<PlanetContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<PlantsService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Agricultural API",
                    Version = "v1"
                });
            });

            builder.Services.AddHttpContextAccessor();


            // Configure Identity
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();


            // Add AutoMapper
            //builder.Services.AddAutoMapper(typeof(MappingConfig));


            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();

            // Add Services
            //builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<IUserService, UserService>();


            // Add Repositories
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IImageRepository, ImageRepository>();

            // Add OpenAPI with Bearer Authentication Support
            //builder.Services.AddOpenApi("v1", options =>
            //{
            //    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            //});

            var app = builder.Build();



            #region Update Database and Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var dbContext = services.GetRequiredService<PlanetContext>();
                    await dbContext.Database.MigrateAsync();
                    await DataSeeder.SeedAsync(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "حدث خطأ أثناء تهيئة قاعدة البيانات أو تهيئة البيانات");
                }
            }
            #endregion

            // Configure the HTTP request pipeline.


            // باقي الكود بتاعك (زي الداتابيز، الـ Services، إلخ)
            // ...

            // الجزء التاني: الـ Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agricultural API V1");
                });
            }

            // تكوين خدمة الملفات الثابتة
         //   app.UseStaticFiles(new StaticFileOptions
         //   {
         //       FileProvider = new PhysicalFileProvider(
         //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
         //       RequestPath = "/images",
         //       OnPrepareResponse = ctx =>
         //       {
         //           ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
         //       }
         //   });


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}














