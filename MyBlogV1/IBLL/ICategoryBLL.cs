using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICategoryBLL
    {
        List<Article> GetCategoryArticles(string categoryId);
        bool CreateANewCategory(string userId, String categoryName);
    }
}
