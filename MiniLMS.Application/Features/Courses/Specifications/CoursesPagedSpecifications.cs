using MiniLMS.Application.Common.Specifications;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Domain.Entities;
using System.Linq.Expressions;

namespace MiniLMS.Application.Features.Courses.Specifications
{
    public class CoursesPagedSpecifications : BaseSpecifications<Course, int>
    {
        public CoursesPagedSpecifications(CourseSpecParams specParams)
            : base(BuildCriteria(specParams))
        {
            AddInclude(c => c.Category);
            AddInclude(c => c.Instructor);
            ApplySorting(specParams.Sort);
            ApplyPagination(specParams.PageIndex, specParams.PageSize);
        }
        private static Expression<Func<Course, bool>> BuildCriteria(CourseSpecParams specParams)
        {
            var search = specParams.Search?.Trim();
            var categoryId = specParams.CategoryId;

            return c =>
                (string.IsNullOrEmpty(search) || c.TitleEn.Contains(search) || c.TitleAr.Contains(search))
                && (categoryId == null || c.CategoryId == categoryId);
        }
        private void ApplySorting(CourseSortBy sortBy)
        {
            switch (sortBy)
            {
                case CourseSortBy.Oldest:
                    ApplyOrderBy(c => c.Id);
                    break;
                case CourseSortBy.TitleAsc:
                    ApplyOrderBy(c => c.TitleEn);
                    break;
                case CourseSortBy.TitleDesc:
                    ApplyOrderByDesc(c => c.TitleEn);
                    break;
                default:
                    ApplyOrderByDesc(c => c.Id);
                    break;   // Newest
            }
        }
    }
}
