using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using WebUI.Models.CategoryViewModels;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                ICategoryBLL categoryBLL = new CategoryBLL();
                string userId = Session["userId"].ToString();
                if(categoryBLL.CreateANewCategory(userId, createCategoryViewModel.Name))
                {
                    return RedirectToAction("CategoryList", new { id = userId });
                } 
            }
            ModelState.AddModelError("", "invalid entry");
            return View(createCategoryViewModel);
        }

        [HttpGet]
        public ActionResult CategoryList(string id)
        {
            ViewBag.Id = id;
            ICategoryBLL categoryBLL = new CategoryBLL();
            var userCategories = categoryBLL.GetEntities(m => m.UserId == id);

            return View(userCategories);
        }

        [HttpGet]
        public ActionResult CategoryDetails(string userId, string id)
        {
            ICategoryBLL categoryBLL = new CategoryBLL();
            Category category = categoryBLL.GetEntity(m => m.Id == id && !m.IsRemoved);

            CategoryDetailsViewModel categoryDetailsViewModel = new CategoryDetailsViewModel 
            { 
                Id = category.Id,
                Name = category.Name,
                CreateDateTime = category.CreateDateTime,
            };

            ViewBag.Previous = userId;

            if(userId == Session["userId"].ToString())
            {
                ViewBag.Same = "true";
                TempData["same"] = "true";
            }
            else
            {
                ViewBag.Same = "false";
                TempData["same"] = "false";
            }

            return View(categoryDetailsViewModel);
        }

        [HttpGet]
        public ActionResult CategoryPageArticles(string id, int pageIndex = 1)
        {
            ICategoryBLL categoryBLL = new CategoryBLL();
            // pager
            // the number of articles per page
            const int pageSize = 3;
            // the total number of articles
            int totalCount;
            string userId = Session["userId"].ToString();

            List<Article> articles = categoryBLL.GetCategoryArticles(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Article>(articles, pageIndex, pageSize, totalCount));
        }

        [HttpGet]
        public ActionResult EditCategory(string id)
        {
            ICategoryBLL categoryBLL = new CategoryBLL();
            Category category = categoryBLL.GetEntity(m => m.Id == id && !m.IsRemoved);
            EditCategoryViewModel editCategoryViewModel = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryViewModel editCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                ICategoryBLL categoryBLL = new CategoryBLL();
                Category category = categoryBLL.GetEntity(m => m.Id == editCategoryViewModel.Id && !m.IsRemoved);
                category.Name = editCategoryViewModel.Name;
                categoryBLL.EditEntity(category);

                return RedirectToAction("CategoryDetails", new { id = category.Id });
            }

            return View(editCategoryViewModel);
        }
    }
}