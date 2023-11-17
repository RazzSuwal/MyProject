﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<Cart> ListOfCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public Course Course { get; set; }

    }
}
