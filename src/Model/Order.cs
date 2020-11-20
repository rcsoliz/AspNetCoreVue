﻿using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Order : Audit
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal  Iva { get; set; }
        public decimal SubTotal{ get; set; }
        public decimal Total { get; set; }

        public List<OrderDetail> Items { get; set; }
    }
}
