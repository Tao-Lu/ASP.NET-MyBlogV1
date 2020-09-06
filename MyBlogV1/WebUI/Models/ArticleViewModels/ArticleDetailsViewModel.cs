using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.ArticleViewModels
{
    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }
        public List<Category> categories { get; set; }
        public Dictionary<Comment, List<Reply>> CommentReplies { get; set; }

        public User ArticleUser { get; set; }
    }
}