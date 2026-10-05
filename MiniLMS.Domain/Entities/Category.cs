using MiniLMS.Domain.Common;

namespace MiniLMS.Domain.Entities;

public class Category : BaseEntity<int>
{

    public string NameEn { get; set; } = string.Empty;

    public string NameAr { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}