﻿using System.Collections.Generic;

namespace STEPÑ.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
