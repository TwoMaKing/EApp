using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EApp.Core;
using EApp.Core.Application;
using Xpress.Chat.Application;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.ServiceContracts;

namespace Xpress.Life.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.HttpContext.Session["user"] = new User()
                                               {
                                                   Id = 1000, 
                                                   Name = "Philips", 
                                                   NickName = "会飞的猪猪", 
                                                   Email = "airsoft_ft@126.com" 
                                               };

            PostDataObject postDataObject = new PostDataObject();
            postDataObject.Topic.Id = 1000;
            postDataObject.Topic.Name = "热门";

            return View(postDataObject);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult PublishPost(int topicId, string content) 
        {
            try
            {
                IPostService postService = ServiceLocator.Instance.GetService<IPostService>();

                PostDataObject postDataObject = new PostDataObject();
                postDataObject.Author.Id = GlobalApplication.LoginUser.Id;
                postDataObject.Author.Name = GlobalApplication.LoginUser.Name;
                postDataObject.Topic.Id = topicId;
                postDataObject.Content = content;
                postDataObject.CreationDateTime = DateTime.Now;

                postService.PublishPost(postDataObject);

                return View("index", postDataObject);

                //return Json(new
                //{
                //    topic = postDataObject.TopicName,
                //    author = postDataObject.AuthorName,
                //    content = postDataObject.Content,
                //    date = postDataObject.CreationDateTime
                //});
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost()]
        public ActionResult PublishPostByCommand(int topicId, string content)
        {
            try
            {
                IPostCommandService postCommandService = ServiceLocator.Instance.GetService<IPostCommandService>();

                PostDataObject postDataObject = new PostDataObject();
                postDataObject.Author.Id = GlobalApplication.LoginUser.Id;
                postDataObject.Author.Name = GlobalApplication.LoginUser.Name;
                postDataObject.Topic.Id = topicId;
                postDataObject.Content = content;
                postDataObject.CreationDateTime = DateTime.Now;

                postCommandService.PublishPost(postDataObject);

                return View("index", postDataObject);

                //return Json(new
                //{
                //    topic = postDataObject.TopicName,
                //    author = postDataObject.AuthorName,
                //    content = postDataObject.Content,
                //    date = postDataObject.CreationDateTime
                //});
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
