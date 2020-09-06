using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// third party pager: webdiyer
using Webdiyer.WebControls.Mvc;
using WebUI.Models.ArticleViewModels;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        [HttpGet]
        public ActionResult CreateArticle()
        {
            string userId = Session["userId"].ToString();

            var categories = new CategoryBLL().GetEntities(m => m.UserId == userId);

            ViewBag.CategoryCount = categories.Count();
            ViewBag.Categories = categories;
            
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            string userId = Session["userId"].ToString();

            var categories = new CategoryBLL().GetEntities(m => m.UserId == userId);

            ViewBag.CategoryCount = categories.Count();
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                IArticleBLL articleBLL = new ArticleBLL();
                if(articleBLL.CreateArticle(createArticleViewModel.Title, createArticleViewModel.Content, createArticleViewModel.CategoryIds, userId))
                {
                    return RedirectToAction("ArticleList", new { id = userId});
                }
            }
            //ModelState.AddModelError("", "add Article Failed");
            return View(createArticleViewModel);
        }

        // third party pager: webdiyer
        [HttpGet]
        public ActionResult ArticleList(string id, int pageIndex = 1)
        {
            ViewBag.Id = id;
            
            IArticleBLL articleBLL = new ArticleBLL();
            // pager
            // the number of articles per page
            const int pageSize = 3;
            // the total number of articles
            int totalCount;

            List<Article> articles = articleBLL.GetPageArticles(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Article>(articles, pageIndex, pageSize, totalCount));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ArticleDetails(string id)
        {
            IArticleBLL articleBLL = new ArticleBLL();
            Article article = articleBLL.GetEntity(m => m.Id == id && !m.IsRemoved);
            if(article != null)
            {
                ArticleDetailsViewModel articleDetailsViewModel = new ArticleDetailsViewModel();
                
                articleDetailsViewModel.Article = article;

                articleDetailsViewModel.categories = articleBLL.GetArticleCategories(id);

                ICommentBLL commentBLL = new CommentBLL();
                Dictionary<Comment, List<Reply>> commentReplies = new Dictionary<Comment, List<Reply>>();
                List<Comment> comments = articleBLL.GetArticleComments(id).OrderByDescending(m => m.CreateDateTime).ToList();
                foreach(Comment comment in comments)
                {
                    List<Reply> replies = new List<Reply>();
                    foreach(Reply reply in commentBLL.GetCommentReplies(comment.Id).OrderByDescending(m => m.CreateDateTime))
                    {
                        replies.Add(reply);
                    }

                    commentReplies.Add(comment, replies);
                    
                }
                articleDetailsViewModel.CommentReplies = commentReplies;

                IUserBLL userBLL = new UserBLL();
                articleDetailsViewModel.ArticleUser = userBLL.GetEntity(m => m.Id == article.UserId && !m.IsRemoved);

                if (Session["userId"] == null)
                {
                    ViewBag.SamePerson = null;
                }
                else if (Session["userId"].ToString() == article.UserId)
                {
                    ViewBag.SamePerson = true;
                }
                else
                {
                    ViewBag.SamePerson = false;
                }
                

                return View(articleDetailsViewModel);
            }

            

            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public ActionResult EditArticle(string id)
        {
            
            IArticleBLL articleBLL = new ArticleBLL();
            Article article = articleBLL.GetEntity(m => m.Id == id && !m.IsRemoved);

            List<string> categoryIds = new List<string>();
            IArticleCategoryIntBLL articleCategoryIntBLL = new ArticleCategoryIntBLL();
            foreach (ArticleCategoryInt articleCategoryInt in articleCategoryIntBLL.GetEntities(m => m.ArticleId == article.Id && !m.IsRemoved))
            {
                categoryIds.Add(articleCategoryInt.Category.Id);
            }

            EditArticleViewModel editArticleViewModel = new EditArticleViewModel
            {
                ArticleId = article.Id,
                Title = article.Title,
                Content = article.Content,
                CategoryIds = categoryIds
            };

            ICategoryBLL categoryBLL = new CategoryBLL();
            string userId = Session["userId"].ToString();
            ViewBag.AllUserCategories = categoryBLL.GetEntities(m => m.UserId == userId);

            return View(editArticleViewModel);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditArticle(EditArticleViewModel editArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                IArticleBLL articleBLL = new ArticleBLL();
                Article article = articleBLL.GetEntity(m => m.Id == editArticleViewModel.ArticleId);
                article.Title = editArticleViewModel.Title;
                article.Content = editArticleViewModel.Content;
                article.CreateDateTime = DateTime.Now;
                articleBLL.EditEntity(article);

                IArticleCategoryIntBLL articleCategoryIntBLL = new ArticleCategoryIntBLL();
                // delete previous categories
                foreach (ArticleCategoryInt articleCategoryInt in articleCategoryIntBLL.GetEntities(m => m.ArticleId == article.Id && !m.IsRemoved))
                {
                    articleCategoryInt.IsRemoved = true;
                }

                // create new categories
                foreach (string id in editArticleViewModel.CategoryIds)
                {
                    articleCategoryIntBLL.AddEntity(new ArticleCategoryInt
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreateDateTime = DateTime.Now,
                        ArticleId = article.Id,
                        CategoryId = id,
                        IsRemoved = false
                    });
                }

                return RedirectToAction("ArticleDetails", new { id = article.Id });
            }
            else
            {
                ICategoryBLL categoryBLL = new CategoryBLL();
                string userId = Session["userId"].ToString();
                ViewBag.AllUserCategories = categoryBLL.GetEntities(m => m.UserId == userId);

                return View(editArticleViewModel);
            }
        }

        [HttpGet]
        public ActionResult LikeArticle(string id)
        {
            IArticleBLL articleBLL = new ArticleBLL();
            Article article = articleBLL.GetEntity(m => m.Id == id && !m.IsRemoved);
            article.LikeCount += 1;
            articleBLL.EditEntity(article);

            return RedirectToAction("ArticleDetails", new { id = id });
        }

        [HttpGet]
        public ActionResult FavoriteArticle(string id)
        {
            string userId = Session["userId"].ToString();
            IFavoriteBLL favoriteBLL = new FavoriteBLL();
            bool contains = false;
            foreach (Favorite favorite in favoriteBLL.GetEntities(m => m.UserId == userId && !m.IsRemoved))
            {
                if(favorite.ArticleId == id)
                {
                    contains = true;
                }
            }

            if (contains)
            {
                // contains already
                TempData["FavoriteAlready"] = "true";
            }
            else
            {
                TempData["FavoriteAlready"] = "false";

                favoriteBLL.AddEntity(new Favorite
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    UserId = userId,
                    ArticleId = id,
                    IsRemoved = false
                });

                IArticleBLL articleBLL = new ArticleBLL();
                Article articel = articleBLL.GetEntity(m => m.Id == id);
                articel.FavoriteCount += 1;
                articleBLL.EditEntity(articel);
            }

            return RedirectToAction("ArticleDetails", new { id = id });
        }

        // third party pager: webdiyer
        [HttpGet]
        public ActionResult FavoriteList(string id, int pageIndex = 1)
        {
            ViewBag.Id = id;
            
            IArticleBLL articleBLL = new ArticleBLL();
            // pager
            // the number of articles per page
            const int pageSize = 3;
            // the total number of articles
            int totalCount;

            List<Article> articles = articleBLL.GetPageFavoriteArticles(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Article>(articles, pageIndex, pageSize, totalCount));
        }
    }
}