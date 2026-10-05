using MiniLMS.Domain.Common;

namespace MiniLMS.Domain.Entities;

public class Enrollment : BaseEntity<int>
{
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public string StudentId { get; set; } = string.Empty;
    public ApplicationUser Student { get; set; } = null!;
}