using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.TitleEn).IsRequired().HasMaxLength(200);
        builder.Property(c => c.TitleAr).IsRequired().HasMaxLength(200);
        builder.Property(c => c.DescriptionEn).IsRequired().HasMaxLength(2000);
        builder.Property(c => c.DescriptionAr).IsRequired().HasMaxLength(2000);
        builder.Property(c => c.ImageUrl).HasMaxLength(500);

        builder.HasOne(c => c.Category)
            .WithMany(cat => cat.Courses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Instructor)
            .WithMany()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.TitleEn);
    }
}