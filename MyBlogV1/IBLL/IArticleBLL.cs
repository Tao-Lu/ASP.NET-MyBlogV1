using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IArticleBLL: IBaseBLL<Article>
    {
        bool CreateArticle(string title, string content, List<string> categoryIds, string userId);
        List<Category> GetArticleCategories(string articleId);
        List<Comment> GetArticleComments(string articleId);
        List<User> GetArticleFavoriteUsers(string articleId);
        User GetArticleUser(string articleId);
        bool AddLikeCount(string articleId);
        bool AddFavoriteCount(string articleId, string userId);

        List<Article> GetPageArticles(string userId, int pageIndex, int pageSize, out int totalCount);

        List<Article> GetPageFavoriteArticles(string userId, int pageIndex, int pageSize, out int totalCount);
    }
}
