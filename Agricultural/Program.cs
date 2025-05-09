using Agricultural.Repo.Data;
using Agricultural.Repo.Data.DataSeeding;
using Agricultural.Repo.Repositories;
using Agricultural.Serv.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;


namespace Agricultural
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            // Get connection string using the helper method from DatabaseHelper
            var connectionString = DatabaseHelper.GetConnectionString(builder.Configuration, builder.Environment);
            Console.WriteLine($"Using connection string: {connectionString}");

            // Add database context
            //options.UseNpgsql(connectionString, npgsqlOptions =>
            builder.Services.AddDbContext<PlanetContext>(options =>
            options.UseNpgsql(connectionString));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<PlantsService>();
            builder.Services.AddScoped<IPlantAdditionalDataService, PlantAdditionalDataService>();
            builder.Services.AddScoped<IPlantResponseService, PlantResponseService>();
            builder.Services.AddScoped<IPlantNameService, PlantNameService>();
            builder.Services.AddScoped<IPlantSearchService, PlantSearchService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Agricultural API",
                    Version = "v1"
                });
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            #region Test Database Connection and Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var dbContext = services.GetRequiredService<PlanetContext>();
                    var canConnect = await dbContext.Database.CanConnectAsync();
                    if (canConnect)
                    {
                        Console.WriteLine("Connection successful!");
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to connect to the database.");
                    }

                    // Apply migrations manually before deployment
                    // Run this command locally to apply migrations:
                    // dotnet ef database update --connection "Host=crossover.proxy.rlwy.net;Port=36690;Database=railway;Username=postgres;Password=DGcdyrkPLsMXwurMtOPBuRNOLmETHtfy;SslMode=Require;TrustServerCertificate=true"

                    // Seed data if needed
                    await DataSeeder.SeedAsync(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "حدث خطأ أثناء الاتصال بقاعدة البيانات أو تهيئة البيانات");
                    throw; // Re-throw to stop the application if connection fails
                }
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agricultural API V1");
                });
            }

            // Configure port
            //uses the default Kestrel configuration

            // Configure middleware for Cors
            app.UseCors("AllowAll");

            // Configure static files
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(wwwrootPath),
                RequestPath = ""
            });

            app.MapControllers();

            app.Run();
        }
    }
}