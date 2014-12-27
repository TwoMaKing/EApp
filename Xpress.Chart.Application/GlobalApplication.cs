using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Xpress.Chart.Domain.Models;

namespace Xpress.Chart.Application
{
    public static class GlobalApplication 
    {
        public static User LoginUser 
        {
            get 
            { 
                object loginUser = HttpContext.Current.Session["user"];

                if (loginUser != null)
                {
                    return (User)loginUser;
                }

                return null;
            }
        }

    }
}
