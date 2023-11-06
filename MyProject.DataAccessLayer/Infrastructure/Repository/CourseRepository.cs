using Microsoft.EntityFrameworkCore;
using MyProject.DataAccessLayer.Data;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
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
    }
}
