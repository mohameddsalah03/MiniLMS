using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniLMS.Domain.Constants;
using MiniLMS.Domain.Entities;
using System.Text.Json;

namespace MiniLMS.Infrastructure.Persistence.Seeding;

public static class DbSeeder
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var sp = scope.ServiceProvider;

        var context = sp.GetRequiredService<AppDbContext>();
        var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = sp.GetRequiredService<IConfiguration>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager, configuration);
        await SeedCategoriesAsync(context);
        await SeedCoursesAsync(context, userManager);
        await SeedEnrollmentsAsync(context, userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in new[] { Roles.Admin, Roles.Instructor, Roles.Student })
        {
            if (!await roleManager.RoleExistsAsync(role))
                ThrowIfFailed(await roleManager.CreateAsync(new IdentityRole(role)));
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        var password = configuration["SeedData:DefaultPassword"]
            ?? throw new InvalidOperationException(
                "Missing 'SeedData:DefaultPassword'. Add it using Manage User Secrets on the API project.");

        var seeds = await ReadJsonAsync<UserSeed>("Users.json");

        foreach (var seed in seeds)
        {
            if (await userManager.FindByEmailAsync(seed.Email) is not null)
                continue;

            var user = new ApplicationUser
            {
                FullName = seed.FullName,
                UserName = seed.Email,
                Email = seed.Email,
                EmailConfirmed = true
            };

            ThrowIfFailed(await userManager.CreateAsync(user, password));
            ThrowIfFailed(await userManager.AddToRoleAsync(user, seed.Role));
        }
    }

    private static async Task SeedCategoriesAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = await ReadJsonAsync<Category>("Categories.json");

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCoursesAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (await context.Courses.AnyAsync())
            return;

        var seeds = await ReadJsonAsync<CourseSeed>("Courses.json");

        var categoryIds = await context.Categories
            .ToDictionaryAsync(c => c.NameEn, c => c.Id, StringComparer.OrdinalIgnoreCase);
        var userIds = await GetUserIdsAsync(userManager);

        var courses = seeds.Select(seed => new Course
        {
            TitleEn = seed.TitleEn,
            TitleAr = seed.TitleAr,
            DescriptionEn = seed.DescriptionEn,
            DescriptionAr = seed.DescriptionAr,
            CategoryId = Resolve(categoryIds, seed.CategoryNameEn, "Category", "Courses.json"),
            InstructorId = Resolve(userIds, seed.InstructorEmail, "User", "Courses.json")
        }).ToList();

        await context.Courses.AddRangeAsync(courses);
        await context.SaveChangesAsync();
    }

    private static async Task SeedEnrollmentsAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (await context.Enrollments.AnyAsync())
            return;

        var seeds = await ReadJsonAsync<EnrollmentSeed>("Enrollments.json");

        var courseIds = await context.Courses
            .ToDictionaryAsync(c => c.TitleEn, c => c.Id, StringComparer.OrdinalIgnoreCase);
        var userIds = await GetUserIdsAsync(userManager);

        var enrollments = seeds.Select(seed => new Enrollment
        {
            CourseId = Resolve(courseIds, seed.CourseTitleEn, "Course", "Enrollments.json"),
            StudentId = Resolve(userIds, seed.StudentEmail, "User", "Enrollments.json")
        }).ToList();

        await context.Enrollments.AddRangeAsync(enrollments);
        await context.SaveChangesAsync();
    }

    #region Helpers

    private static async Task<List<T>> ReadJsonAsync<T>(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seeding", "Data", fileName);

        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Seed file '{fileName}' was not found. Check 'Copy to Output Directory' for the JSON files.", path);

        await using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream, JsonOptions) ?? new List<T>();
    }

    private static async Task<Dictionary<string, string>> GetUserIdsAsync(UserManager<ApplicationUser> userManager)
        => await userManager.Users.ToDictionaryAsync(u => u.Email!, u => u.Id, StringComparer.OrdinalIgnoreCase);

    private static TValue Resolve<TValue>(Dictionary<string, TValue> lookup, string key, string entityName, string fileName)
        => lookup.TryGetValue(key, out var value)
            ? value
            : throw new InvalidOperationException($"{entityName} '{key}' referenced in {fileName} was not found.");

    private static void ThrowIfFailed(IdentityResult result)
    {
        if (!result.Succeeded)
            throw new InvalidOperationException(
                "Seeding failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }

    #endregion
}

internal sealed record UserSeed(string FullName, string Email, string Role);

internal sealed record CourseSeed(
    string TitleEn, string TitleAr,
    string DescriptionEn, string DescriptionAr,
    string CategoryNameEn, string InstructorEmail);

internal sealed record EnrollmentSeed(string CourseTitleEn, string StudentEmail);