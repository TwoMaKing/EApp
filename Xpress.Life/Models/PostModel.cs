using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xpress.Chat.DataObjects;

namespace Xpress.Life.Models
{
    public class PostModel
    {
        private static Dictionary<DateTime, List<PostDataObject>> postsByDate = new Dictionary<DateTime, List<PostDataObject>>();
        static PostModel() 
        { 
            List<PostDataObject> posts1 = new List<PostDataObject>();
            var p1 = new PostDataObject() { Id = 1000, Content = "LALALALALALA" };
            p1.Author.Name = "SB";
            var p2 = new PostDataObject() { Id = 1001, Content = "XXXXXXXXXXXX" };
            p2.Author.Name = "ZYL";
            posts1.AddRange(new PostDataObject[] { p1, p2 });

            List<PostDataObject> posts2 = new List<PostDataObject>();
            var p3= new PostDataObject() { Id = 1002, Content = "LOLOLOLOLOLO" };
            p3.Author.Name = "YSY";
            var p4 = new PostDataObject() { Id = 1003, Content = "AIAIAIAIAIAIAI" };
            p4.Author.Name = "GQ";
            posts2.AddRange(new PostDataObject[] { p3, p4 });

            postsByDate.Add(DateTime.Parse("2015-03-11"), posts1);
            postsByDate.Add(DateTime.Parse("2015-03-12"), posts2);

        }

        public static Dictionary<DateTime, List<PostDataObject>> PostsByDate 
        { 
            get 
            {
                return postsByDate;
            } 
        }
    }
}