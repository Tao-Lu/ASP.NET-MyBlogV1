using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.CommentViewModel
{
    public class CreateCommentViewModel
    {
        public string ArticleId { get; set; }
        public string Content { get; set; }
    }
}