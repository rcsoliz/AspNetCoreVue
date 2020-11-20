﻿using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Category : Audit
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
