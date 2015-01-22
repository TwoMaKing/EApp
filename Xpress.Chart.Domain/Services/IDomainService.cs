using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.Domain.Services
{
    /// <summary>
    /// This project domain service
    /// </summary>
    public interface IDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Post PublishPost(Topic topic, User author, string content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="post"></param>
        void PublishComment(Comment comment, Post post);

    }
}
