using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NTDomain { get; set; }

        public string NTAccount { get; set; }

        public string Email { get; set; }
    }
}
