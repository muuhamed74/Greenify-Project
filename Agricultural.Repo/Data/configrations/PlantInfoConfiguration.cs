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

            builder.Property(p => p.SeasonType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.SoilType)
                .IsRequired()
                .HasMaxLength(50);

            // القيم الرقمية (درجة الحرارة والرطوبة)
            builder.Property(p => p.TemperatureMin)
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.TemperatureMax)
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.HumidityMin)
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.HumidityMax)
                .HasColumnType("decimal(5,2)");

            // تحديد العلاقة بين `PlantInfo` و `PlantImages`
            builder.HasMany(p => p.PlantImages)
                .WithOne(i => i.PlantsInfo)
                .HasForeignKey(i => i.PlantsInfoId)
                .OnDelete(DeleteBehavior.Cascade); // لو تم حذف النبات، نحذف الصور الخاصة به أيضًا
        }
    }

}

