using BLL;
using IBLL;
using Microsoft.Ajax.Utilities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Models.UserViewModels;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                IUserBLL userBLL = new UserBLL();
                if(userBLL.Register(registerViewModel.Email, registerViewModel.Password))
                {
                    return RedirectToAction("Login");
                }
            }
            
            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = loginViewModel.Email;
                string password = loginViewModel.Password;
                string userId;
                List<string> roles = new List<string>();
                IUserBLL userBLL = new UserBLL();
                if(userBLL.Login(email, password, out userId, ref roles))
                {
                    // set Authen Cookie
                    SetAuthCookie(userId, roles);

                    // Cookie
                    if (loginViewModel.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value = userId,
                            Expires = DateTime.Now.AddDays(7)
                        });

                        Response.Cookies.Add(new HttpCookie("userRoles")
                        {
                            Value = String.Join(",", roles),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    // Session
                    else
                    {
                        Session["userId"] = userId;
                        Session["userRoles"] = String.Join(",", roles);
                    }

                    //return to previous url
                    string returnUrl = FormsAuthentication.GetRedirectUrl("qq", true);

                    return Redirect(returnUrl);
                }
            }
            ModelState.AddModelError("", "wrong email or password");
            return View(loginViewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            Session.Abandon();
            //HttpContext.Request.Cookies["userId"].Expires;

            return RedirectToAction("Login");
        }

        // set Authen Cookie
        public void SetAuthCookie(string userId, List<string> roles)
        {
            string userData = userId + "#" + String.Join(",", roles.ToArray());

            IUserBLL userBLL = new UserBLL();
            User user = userBLL.GetEntity(m => m.Id == userId);

            // creates a ticket
            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
            // encrypts the ticket
            string encryptedTicket = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            // creates a cookie using an encrypted ticket
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            // adds cookie to http response
            HttpContext.Response.Cookies.Add(httpCookie);

        }

        // user admin
        [HttpGet]
        public ActionResult UserPortal(string id)
        {
            ViewBag.Id = id;
            IUserBLL userBLL = new UserBLL();
            ViewBag.Name = userBLL.GetEntity(m => m.Id == id && !m.IsRemoved).Name;
            return View();
        }

        [HttpGet]
        public ActionResult UserDetails(string id)
        {
            IUserBLL userBLL = new UserBLL();
            User user = userBLL.GetEntity(m => m.Id == id && !m.IsRemoved);

            IArticleBLL articleBLL = new ArticleBLL();
            int totalArticleCount = articleBLL.GetEntities(m => m.UserId == user.Id && !m.IsRemoved).Count();

            ICategoryBLL categoryBLL = new CategoryBLL();
            int totalCategoryCount = categoryBLL.GetEntities(m => m.UserId == user.Id).Count();

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                CreateDateTime = user.CreateDateTime,
                FollowerCount = user.FollowerCount,
                FollowingCount = user.FollowingCount,
                TotalArticleCount = totalArticleCount,
                TotalCategoryCount = totalCategoryCount
            };

            return View(userDetailsViewModel);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            IUserBLL userBLL = new UserBLL();
            User user = userBLL.GetEntity(m => m.Id == id && !m.IsRemoved);
            EditUserViewModel editUserViewModel = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                IUserBLL userBLL = new UserBLL();
                User user = userBLL.GetEntity(m => m.Id == editUserViewModel.Id && !m.IsRemoved);
                user.Name = editUserViewModel.Name;
                userBLL.EditEntity(user);

                return RedirectToAction("UserDetails");
            }
            
            return View(editUserViewModel);
        }

        [HttpGet]
        public ActionResult Follow(string articleId, string followingUserId)
        {
            string currentUserId = Session["userId"].ToString();
            IUserBLL userBLL = new UserBLL();
            if (userBLL.AddFollowing(currentUserId, followingUserId))
            {
                TempData["Follow"] = "true";
            }
            else
            {
                TempData["Follow"] = "false";
            }

            return RedirectToAction("ArticleDetails", "Article", new { id = articleId });
        }

        [HttpGet]
        public ActionResult GetFollowing(string id)
        {
            IUserBLL userBLL = new UserBLL();
            List<User> users = userBLL.GetUserFollowings(id);
            ViewBag.Following = users;
            
            return View();
        }

        [HttpGet]
        public ActionResult GetFollower(string id)
        {
            IUserBLL userBLL = new UserBLL();
            List<User> users = userBLL.GetUserFollowers(id);
            ViewBag.Follower = users;

            return View();
        }
    }
}