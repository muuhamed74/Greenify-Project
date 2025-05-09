
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;




namespace Agricultural.Repo.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlanetContext>
    {
        public PlanetContext CreateDbContext(string[] args)
        {


            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            // تحميل إعدادات التكوين
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<PlanetContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("❌ لم يتم العثور على `ConnectionString` في `appsettings.json`");
            }
            builder.UseNpgsql(connectionString);
            //builder.UseSqlServer(connectionString);

            Console.WriteLine("✅ تم تحميل `PlanetContext` بنجاح!");
            return new PlanetContext(builder.Options);
        }
    }
}
