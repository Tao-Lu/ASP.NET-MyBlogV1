using BLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.CommentViewModel;

namespace WebUI.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        public void CreateComment(CreateCommentViewModel createCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = Session["userId"].ToString();
                ICommentBLL commentBLL = new CommentBLL();
                commentBLL.AddEntity(new Model.Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    Content = createCommentViewModel.Content,
                    ArticleId = createCommentViewModel.ArticleId,
                    UserId = userId,
                    IsRemoved = false
                });

            }

            ModelState.AddModelError("", "invalid entry");
        }
    }
}