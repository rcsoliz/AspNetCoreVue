using System.Collections.Generic;
using System.Linq;

namespace Service.Comomns
{
    public class DataCollection<T> where T: class
    {
        public bool HasItems
        {
            get
            {
                return Items != null && Items.Any();
            }
        }

        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Page  { get; set; }
        public decimal Pages  { get; set; }

    }
}
