using Microsoft.EntityFrameworkCore;
using MyProject.DataAccessLayer.Data;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.DataAccessLayer.Migrations;
using MyProject.Models;
using MyProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.Infrastructure.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Course course)
        {
            var courseDB = _context.Courses.FirstOrDefault(x => x.Id == course.Id);
            if (courseDB != null)
            {
                courseDB.Name = course.Name;
                courseDB.Description = course.Description;
                courseDB.Price = course.Price;
                if (course.ImageUrl != null)
                {
                    courseDB.ImageUrl = course.ImageUrl;
                }
                //if (course.VideoUrl != null)
                //{
                //    courseDB.VideoUrl = course.VideoUrl;
                //}
            }
        }
        public IEnumerable<Course> GetSimilarCourses(int courseId)
        {
            var targetCourse = _context.Courses.FirstOrDefault(x => x.Id == courseId);

            if (targetCourse == null)
            {
                return Enumerable.Empty<Course>();
            }

            var similarCourses = _context.Set<Course>()
                .Where(c => c.Id != courseId && c.CategoryId == targetCourse.CategoryId)
                .OrderByDescending(c => CosineSimilarity(targetCourse.Description, c.Description))
                .Take(5)
                .ToList();

            return similarCourses;
        }

        // Assuming you have a DbSet<Category> in your DbContext
        private double CosineSimilarity(string text1, string text2)
        {
            var vector1 = text1.Split(' '); // Split text into words
            var vector2 = text2.Split(' ');

            var commonFeatures = vector1.Intersect(vector2).ToList();
            double dotProduct = commonFeatures.Count;

            double magnitude1 = Math.Sqrt(vector1.Length);
            double magnitude2 = Math.Sqrt(vector2.Length);

            if (magnitude1 == 0 || magnitude2 == 0)
            {
                return 0; // To avoid division by zero
            }

            return dotProduct / (magnitude1 * magnitude2);
        }
    }
}
