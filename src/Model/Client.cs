using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Client : Audit
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
