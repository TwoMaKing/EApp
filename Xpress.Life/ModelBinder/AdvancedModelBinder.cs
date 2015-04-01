using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Xpress.Chat.DataObjects;

namespace Xpress.Life.ModelBinder
{
    public class PostModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            PostDataObject post = (PostDataObject)(bindingContext.Model ?? new PostDataObject());

            string searchPrefix = string.Empty;
            // find out if the value provider has the required prefix
            //bool hasPrefix = bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName);   // bindingContext.ModelName返回当前模型的名称

            //string searchPrefix = (hasPrefix) ? bindingContext.ModelName + "." : "";

            post.Topic.Id = GetFieldValue<int>(bindingContext.ValueProvider, searchPrefix + "Topic.Id");

            post.Content = GetFieldValue<string>(bindingContext.ValueProvider, searchPrefix + "Content");

            post.CreationDateTime = DateTime.Now;

            return post;
        }

        private T GetFieldValue<T>(IValueProvider valueProvider, string key)
        {
            ValueProviderResult r = valueProvider.GetValue(key);

            return (T)r.ConvertTo(typeof(T));
        }

    }
}
