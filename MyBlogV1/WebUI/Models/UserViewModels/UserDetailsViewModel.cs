using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.UserViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        //public string ProfilePicPath { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int TotalArticleCount { get; set; }
        public int TotalCategoryCount { get; set; }
    }
}