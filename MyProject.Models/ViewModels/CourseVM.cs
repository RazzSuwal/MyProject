using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models.ViewModels
{
    public class CourseVM
    {
        public Course Course { get; set; } = new Course();
        [ValidateNever]
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }
        public IEnumerable<Course> ListOfCourse { get; set; }
    }
}
