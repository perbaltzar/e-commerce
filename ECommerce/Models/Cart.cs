﻿using System;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public Cart()
        {
        }
    }
}
