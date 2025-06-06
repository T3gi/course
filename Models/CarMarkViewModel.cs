﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Phoenix.Models
{
    public class CarViewModel
    {
        public List<Car>? Cars { get; set; }
        public SelectList? Marks { get; set; }
        public SelectList? Brands { get; set; }
        public string? CarMark { get; set; }
        public string? CarBrand { get; set; }
        public string? SearchString { get; set; }
    }
}
