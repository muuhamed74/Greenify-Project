using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agricultural.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agricultural.Repo.Data.configrations
{
    public class PlantInfoConfiguration : IEntityTypeConfiguration<PlantsInfo>
    {
        public void Configure(EntityTypeBuilder<PlantsInfo> builder)
        {
            // تعيين الـ Primary Key
            builder.HasKey(p => p.Id);

            // تحديد اسم الجدول في قاعدة البيانات
            builder.ToTable("PlantInfos");

            // خصائص النصوص
            builder.Property(p => p.PlantName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ScientificName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CareLevel)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Size)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Edibility)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.About)
                .IsRequired();

            // تكوين العلاقة مع PlantDetails
            builder.OwnsOne(p => p.Details, details =>
            {
                details.Property(d => d.Temperature).IsRequired();
                details.Property(d => d.Sunlight).IsRequired();
                details.Property(d => d.Water).IsRequired();
                details.Property(d => d.Repotting).IsRequired();
                details.Property(d => d.Fertilizing).IsRequired();
                details.Property(d => d.Pests).IsRequired();
            });

            // تحديد العلاقة بين `PlantInfo` و `PlantImages`
            builder.HasMany(p => p.PlantImages)
                .WithOne(i => i.PlantsInfo)
                .HasForeignKey(i => i.PlantsInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

