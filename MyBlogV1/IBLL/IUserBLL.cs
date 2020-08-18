using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserBLL: IBaseBLL<User>
    {
        bool Register(string email, string password);
        bool Login(string email, string password);
        bool UpdatePassword(string userId, string oldPassword, string newPassword);
        List<User> GetUserFollowers(string userId);
        List<User> GetUserFollowings(string userId);
        List<Article> GetUserFavoriteArticles(string userId);
        List<Role> GetUserRoles(string userId);
        List<Article> GetUserArticles(string userId);
        List<Category> GetUserCategories(string userId);
        List<Comment> GetUserComments(string userId);
        List<Reply> GetUserReplies(string userId);
        bool AddFollowing(string currentUserId, string followingUserId);
    }
}
