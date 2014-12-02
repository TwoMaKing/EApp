using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Core.Entities
{
    public class JobType
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public decimal UtilRate { get; set; }

        public decimal BaseAmount { get; set; }
    }
}
