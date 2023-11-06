using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader OrderHeader{ get; set; }
        [Required]
        public int CourseId { get; set; }
        [ValidateNever]
        public Course Course { get; set; }
        public double Price { get; set; }
    }
}
