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
    internal class PlantImagesConfigrations : IEntityTypeConfiguration<PlantImages>
    {
        
            public void Configure(EntityTypeBuilder<PlantImages> builder)
            {
                builder.HasKey(i => i.Id);

                builder.ToTable("PlantImages");

                builder.Property(i => i.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(255); // تأكد أن الصورة ليها طول مناسب

                // العلاقة مع `PlantInfo`
                builder.HasOne(i => i.PlantsInfo)
                    .WithMany(p => p.PlantImages)
                    .HasForeignKey(i => i.PlantsInfoId);
            }
        }

    }

