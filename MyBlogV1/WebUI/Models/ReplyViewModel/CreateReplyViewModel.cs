using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.ReplyViewModel
{
    public class CreateReplyViewModel
    {
        public string CommentId { get; set; }
        public string ReplyToUserId { get; set; }
        public string Content { get; set; }
    }
}