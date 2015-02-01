using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Commands
{
    [Serializable()]
    public class Command : ICommand
    {
        public int Id
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.Id.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == null ||
               !(obj is Command))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            Command otherCommand = obj as Command;

            return this.Id == otherCommand.Id;
        }

    }
}
