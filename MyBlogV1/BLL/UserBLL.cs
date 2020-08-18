using DBSession;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL : BaseBLL<User>, IUserBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.UserDAL;
        }


        public bool AddFollowing(string currentUserId, string followingUserId)
        {
            User currentUser = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == currentUserId);
            User followingUser = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == followingUserId);

            if (currentUser != null && followingUser != null)
            {
                currentUser.FollowingCount += 1;
                followingUser.FollowerCount += 1;
                dbSession.UserDAL.EditEntity(currentUser);
                dbSession.UserDAL.EditEntity(followingUser);

                Following newFollowing = new Following
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    UserId = currentUserId,
                    FollowingUserId = followingUserId,
                    IsRemoved = false
                };
                dbSession.FollowingDAL.AddEntity(newFollowing);

                return dbSession.SaveChanges();
            }

            return false;
        }

        public List<Article> GetUserArticles(string userId)
        {
            List<Article> articles = new List<Article>();

            foreach(Article article in dbSession.ArticleDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                articles.Add(article);
            }

            return articles;
        }

        public List<Category> GetUserCategories(string userId)
        {
            List<Category> categories = new List<Category>();

            foreach(Category category in dbSession.CategoryDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                categories.Add(category);
            }

            return categories;
        }

        public List<Comment> GetUserComments(string userId)
        {
            List<Comment> comments = new List<Comment>();

            foreach(Comment comment in dbSession.CommentDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                comments.Add(comment);
            }

            return comments;
        }

        public List<Article> GetUserFavoriteArticles(string userId)
        {
            List<Article> articles = new List<Article>();

            foreach(Favorite favorite in dbSession.FavoriteDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                Article article = dbSession.ArticleDAL.GetEntity(m => !m.IsRemoved && m.Id == favorite.ArticleId);
                if(article != null)
                {
                    articles.Add(article);
                }
            }

            return articles;  
        }

        public List<User> GetUserFollowers(string userId)
        {
            List<User> users = new List<User>();

            foreach (Following following in dbSession.FollowingDAL.GetEntities(m => !m.IsRemoved && m.FollowingUserId == userId))
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == following.UserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public List<User> GetUserFollowings(string userId)
        {
            List<User> users = new List<User>();

            foreach (Following following in dbSession.FollowingDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == following.FollowingUserId);
                if(user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public List<Reply> GetUserReplies(string userId)
        {
            List<Reply> replies = new List<Reply>();

            foreach (Reply reply in dbSession.ReplyDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                replies.Add(reply);
            }

            return replies;
        }

        public List<Role> GetUserRoles(string userId)
        {
            List<Role> roles = new List<Role>();

            foreach (UserRoleInt userRoleInt in dbSession.UserRoleIntDAL.GetEntities(m => !m.IsRemoved && m.UserId == userId))
            {
                Role role = dbSession.RoleDAL.GetEntity(m => !m.IsRemoved && m.Id == userRoleInt.RoleId);
                if(role != null)
                {
                    roles.Add(role);
                }
            }

            return roles;
        }

        public bool Login(string email, string password)
        {
            User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Email == email);
            if(user != null)
            {
                return user.PasswordEncrypted == MD5Encryption64(password);
            }

            return false;
        }

        public bool UpdatePassword(string userId, string oldPassword, string newPassword)
        {
            User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == userId);
            if(user != null)
            {
                if(user.PasswordEncrypted == MD5Encryption64(oldPassword))
                {
                    user.PasswordEncrypted = MD5Encryption64(newPassword);
                    dbSession.UserDAL.EditEntity(user);
                    return dbSession.SaveChanges();
                }
            }

            return false;
        }

        public bool Register(string email, string password)
        {
            User user = dbSession.UserDAL.GetEntity(m => m.Email == email);
            // user does not exist in database
            if (user == null)
            {
                User newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    Name = email.Split('@')[0],
                    PasswordEncrypted = MD5Encryption64(password),
                    CreateDateTime = DateTime.Now,
                    ProfilePicPath = "ProfilePics/default.jpg",
                    FollowerCount = 0,
                    FollowingCount = 0,
                    IsRemoved = false
                };
                dbSession.UserDAL.AddEntity(user);

                // assigns a role to the user
                //UserRoleInt userRoleInt = new UserRoleInt
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    UserId = newUser.Id,
                //    RoleId = ,
                //    StartDateTime = DateTime.Now,
                //    EndDateTime = null,
                //    IsRemoved = false
                //};
                //dbSession.UserRoleIntDAL.AddEntity(userRoleInt);

                return dbSession.SaveChanges();
            }
            else if (user.IsRemoved)
            {
                user.Name = email.Split('@')[0];
                user.PasswordEncrypted = MD5Encryption64(password);
                user.CreateDateTime = DateTime.Now;
                user.ProfilePicPath = "ProfilePics/default.jpg";
                user.FollowerCount = 0;
                user.FollowingCount = 0;
                user.IsRemoved = false;
                dbSession.UserDAL.EditEntity(user);

                return dbSession.SaveChanges();
            }

            return false;
        }

        private string MD5Encryption64(string password)
        {
            MD5 mD5 = MD5.Create();
            byte[] s = mD5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(s);
        }
    }
}
