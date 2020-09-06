using BLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.ReplyViewModel;

namespace WebUI.Controllers
{
    public class ReplyController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        public void CreateReply(CreateReplyViewModel createReplyViewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = Session["userId"].ToString();
                IReplyBLL replyBLL = new ReplyBLL();
                replyBLL.AddEntity(new Model.Reply
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    Content = createReplyViewModel.Content,
                    CommentId = createReplyViewModel.CommentId,
                    UserId = userId,
                    ReplyToUserId = createReplyViewModel.ReplyToUserId,
                    IsRemoved = false
                });
                
            }
            else
            {
                ModelState.AddModelError("", "invalid entry");
            }

            
        }
    }
}