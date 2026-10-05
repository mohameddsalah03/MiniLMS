using MiniLMS.Domain.Common;

namespace MiniLMS.Domain.Entities;

public class Course : BaseEntity<int>
{

    public string TitleEn { get; set; } = string.Empty;

    public string TitleAr { get; set; } = string.Empty;

    public string DescriptionEn { get; set; } = string.Empty;

    public string DescriptionAr { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key + navigation: Category
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Foreign key + navigation: Instructor (a user)
    public string InstructorId { get; set; } = string.Empty;
    public ApplicationUser Instructor { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}