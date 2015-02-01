using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using EApp.Domain.Core.Application;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.DataObjects
{
    [DataContract()]
    public class UserDataObject : DataTransferObjectBase<User>
    {
        public UserDataObject() { }

        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public string NickName { get; set; }
        
        [DataMember()]
        public string Email { get; set; }

        [DataMember()]
        public string Password { get; set; }

        protected override void DoMapFrom(User domainModel)
        {
            this.Id = domainModel.Id;
            this.Name = domainModel.Name;
            this.NickName = domainModel.NickName;
            this.Email = domainModel.Email;
            this.Password = domainModel.Password;
        }

        protected override User DoMapTo()
        {
            User user = new User();

            user.Id = this.Id;
            user.Name = this.Name;
            user.NickName = this.NickName;
            user.Email = this.Email;
            user.Password = this.Password;

            return user;
        }
    }
}
