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
    public class CategoryBLL : BaseBLL<Category>, ICategoryBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.CategoryDAL;
        }

        public List<Article> GetCategoryArticles(string categoryId, int pageIndex, int pageSize, out int totalCount)
        {
            List<Article> articles = new List<Article>();

            foreach (ArticleCategoryInt articleCategoryInt in dbSession.ArticleCategoryIntDAL.GetEntitiesByPageOrdered(pageIndex, pageSize, out totalCount, m => !m.IsRemoved && m.CategoryId == categoryId, m => m.CreateDateTime, false))
            {
                Article article = dbSession.ArticleDAL.GetEntity(m => !m.IsRemoved && m.Id == articleCategoryInt.ArticleId);
                if (article != null)
                {
                    articles.Add(article);
                }
            }

            return articles;
        }

        public bool CreateANewCategory(string userId, string categoryName)
        {
            Category category = dbSession.CategoryDAL.GetEntity(m => m.Name == categoryName && m.UserId == userId);
            // a new category name
            if(category == null)
            {
                Category newCategory = new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = categoryName,
                    CreateDateTime = DateTime.Now,
                    UserId = userId,
                    IsRemoved = false
                };
                dbSession.CategoryDAL.AddEntity(newCategory);
                return dbSession.SaveChanges();
            }
            else if (category.IsRemoved)
            {
                category.IsRemoved = false;
                category.CreateDateTime = DateTime.Now;
                dbSession.CategoryDAL.EditEntity(category);
                return dbSession.SaveChanges();
            }

            return false;
        }
    }
}
