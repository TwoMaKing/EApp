using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Xpress.Mvc.Models
{
    public class CostModel
    {
        public IEnumerable<CostLine> Costs { get; set; }
    }

    public class CostLine 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class Order 
    { 
    
    }

    public class OrderQueryRequest 
    {
        public int Id { get; set; }

        public string HostInfo { get; set; }
    }

}
