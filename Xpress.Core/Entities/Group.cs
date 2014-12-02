using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Core.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<Subgroup> Subgroups { get; set; }

    }
}
