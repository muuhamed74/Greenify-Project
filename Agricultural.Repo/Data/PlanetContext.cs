
using Microsoft.EntityFrameworkCore;
using Agricultural.Core.Models;
using Agricultural.Repo.Data.configrations;

namespace Agricultural.Repo.Data
{
    public class PlanetContext : DbContext
    {
        public PlanetContext(DbContextOptions<PlanetContext> options) : base(options)
        {

        }

        public DbSet<PlantsInfo> PlantsInfo { get; set; }
        public DbSet<PlantImages> PlantImages { get; set; }
        public DbSet<PlantAdditionalData> PlantAdditionalData { get; set; }
        public DbSet<Plant> Plant { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlantInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PlantImagesConfigrations());
            base.OnModelCreating(modelBuilder);
        }


    }
}
