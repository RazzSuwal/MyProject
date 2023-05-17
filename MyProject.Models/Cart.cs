using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ValidateNever]
        public Course Course { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        [ForeignKey("ApplicationUserId")]

        public ApplicationUser ApplicationUser { get; set; }
      
    }
}
