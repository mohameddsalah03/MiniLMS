namespace MiniLMS.Application.Features.Courses.DTOs
{
    public class CourseSpecParams
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public CourseSortBy Sort { get; set; } = CourseSortBy.Newest;
    }
}