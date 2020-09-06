using DBSession;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ArticleBLL : BaseBLL<Article>, IArticleBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.ArticleDAL;
        }

        public bool AddFavoriteCount(string articleId, string userId)
        {
            Article article = dbSession.ArticleDAL.GetEntity(m => !m.IsRemoved && m.Id == articleId);

            if(article != null)
            {
                article.FavoriteCount += 1;
                dbSession.ArticleDAL.EditEntity(article);

                Favorite favorite = new Favorite
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    UserId = userId,
                    ArticleId = articleId,
                    IsRemoved = false
                };
                dbSession.FavoriteDAL.AddEntity(favorite);

                return dbSession.SaveChanges();
            }

            return false;
        }

        public bool AddLikeCount(string articleId)
        {
            Article article = dbSession.ArticleDAL.GetEntity(m => !m.IsRemoved && m.Id == articleId);
            
            if(article != null)
            {
                article.LikeCount += 1;
                dbSession.ArticleDAL.EditEntity(article);

                return dbSession.SaveChanges();
            }

            return false;
        }

        public List<Category> GetArticleCategories(string articleId)
        {
            List<Category> categories = new List<Category>();

            foreach(ArticleCategoryInt articleCategoryInt in dbSession.ArticleCategoryIntDAL.GetEntities(m => !m.IsRemoved && m.ArticleId == articleId))
            {
                Category category = dbSession.CategoryDAL.GetEntity(m => !m.IsRemoved && m.Id == articleCategoryInt.CategoryId);
                if (category != null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }

        public List<Comment> GetArticleComments(string articleId)
        {
            List<Comment> comments = new List<Comment>();

            foreach(Comment comment in dbSession.CommentDAL.GetEntities(m => !m.IsRemoved && m.ArticleId == articleId))
            {
                comments.Add(comment);
            }

            return comments;
        }

        public List<User> GetArticleFavoriteUsers(string articleId)
        {
            List<User> users = new List<User>();

            foreach (Favorite favorite in dbSession.FavoriteDAL.GetEntities(m => !m.IsRemoved && m.ArticleId == articleId))
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == favorite.UserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public User GetArticleUser(string articleId)
        {
            Article article = dbSession.ArticleDAL.GetEntity(m => !m.IsRemoved && m.Id == articleId);
            if(article != null)
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == article.UserId);
                if(user != null)
                {
                    return user;
                }
            }

            return null;
        }

        public bool CreateArticle(string title, string content, List<string> categoryIds, string userId)
        {
            Article article = new Article
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Content = content,
                CreateDateTime = DateTime.Now,
                LikeCount = 0,
                FavoriteCount = 0,
                UserId = userId,
                IsRemoved = false
            };
            dbSession.ArticleDAL.AddEntity(article);

            foreach(string categoryId in categoryIds)
            {
                dbSession.ArticleCategoryIntDAL.AddEntity(new ArticleCategoryInt
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDateTime = DateTime.Now,
                    ArticleId = article.Id,
                    CategoryId = categoryId,
                    IsRemoved = false
                });
            }

            return dbSession.SaveChanges();
        }

        public List<Article> GetPageArticles(string userId, int pageIndex, int pageSize, out int totalCount)
        {
            List<Article> articles = new List<Article>();
            foreach(Article article in dbSession.ArticleDAL.GetEntitiesByPageOrdered(pageIndex, pageSize, out totalCount, m => m.UserId == userId && !m.IsRemoved, m => m.CreateDateTime, false))
            {
                articles.Add(article);
            }

            return articles;
        }

        public List<Article> GetPageFavoriteArticles(string userId, int pageIndex, int pageSize, out int totalCount)
        {
            List<Article> articles = new List<Article>();

            foreach (Favorite favorite in dbSession.FavoriteDAL.GetEntitiesByPageOrdered(pageIndex, pageSize, out totalCount, m => m.UserId == userId && !m.IsRemoved, m => m.CreateDateTime, false))
            {
                articles.Add(favorite.Article);
            }

            return articles;
        }
    }
}
