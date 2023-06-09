﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Rating { get; set; }

        //ForeignKey
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        public string TeacherId { get; set; }
        [ValidateNever]
        public ApplicationUser Teacher { get; set; }
    }
}
