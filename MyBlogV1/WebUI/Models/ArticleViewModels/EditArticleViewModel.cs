using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.ArticleViewModels
{
    public class EditArticleViewModel
    {
        public string ArticleId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Categories")]
        public List<string> CategoryIds { get; set; }
    }
}