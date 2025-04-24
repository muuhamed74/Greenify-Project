using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<Users> Users { get; set; }
        public DbSet<PlantPrediction> PlantPrediction { get; set; }
        public DbSet<Uploaded_Images> UploadedImages { get; set; }
        public DbSet<PlantAdditionalData> PlantAdditionalData { get; set; }
        public DbSet<PlantResponse> PlantResponse { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlantInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PlantImagesConfigrations());
            base.OnModelCreating(modelBuilder);
        }


    }
}
