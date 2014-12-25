using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class User : EntityBase, IAggregateRoot
    {
        public User() : base() { }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static User Create(string name, string email, string password) 
        {
            User user = new User();

            user.Name = name;
            user.NickName = name;
            user.Email = email;
            user.Password = password;

            return user;
        }
    }
}
