using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMall.Shared
{
    public class Product
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public Decimal Price { get; set; }
        public String Description { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
