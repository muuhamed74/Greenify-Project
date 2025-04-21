using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


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

            builder.UseSqlServer(connectionString);

            Console.WriteLine("✅ تم تحميل `PlanetContext` بنجاح!");
            return new PlanetContext(builder.Options);
        }
    }
}
