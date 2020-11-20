using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Country : Audit
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}
