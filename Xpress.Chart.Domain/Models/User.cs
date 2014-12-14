using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class User : EntityBase, IAggregateRoot
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
