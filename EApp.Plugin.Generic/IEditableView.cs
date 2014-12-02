using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Plugin.Generic
{
    public interface IEditableView : IView
    {
        bool ViewChanged { get; }

        void PopulateView();
    }
}
