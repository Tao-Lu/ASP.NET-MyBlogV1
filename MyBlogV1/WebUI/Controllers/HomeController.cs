using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IArticleBLL articleBLL = new ArticleBLL();
            List<Article> articles = articleBLL.GetEntities(m => !m.IsRemoved).ToList();

            // top 6 new articles
            ViewBag.newArticles = articles.OrderByDescending(m => m.CreateDateTime).Take(6).ToArray();

            // top 6 most like articles
            ViewBag.mostLikeArticles = articles.OrderByDescending(m => m.LikeCount).Take(6).ToArray();

            // top 6 most favorite articles
            ViewBag.mostFavoriteArticles = articles.OrderByDescending(m => m.FavoriteCount).Take(6).ToArray();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}