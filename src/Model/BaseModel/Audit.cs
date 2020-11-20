using System;
using System.Collections.Generic;
using System.Text;

namespace Model.BaseModel
{
    public class Audit
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
