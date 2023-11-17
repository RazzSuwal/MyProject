using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.Infrastructure.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        void Update(Course course);
        IEnumerable<Course> GetSimilarCourses(int courseId);
    }
}

