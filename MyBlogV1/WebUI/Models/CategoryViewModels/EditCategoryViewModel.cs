﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.CategoryViewModels
{
    public class EditCategoryViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}