using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Product : Audit
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Caterory { get; set; }
    }
}
