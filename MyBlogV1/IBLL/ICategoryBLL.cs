using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICategoryBLL: IBaseBLL<Category>
    {
        List<Article> GetCategoryArticles(string categoryId, int pageIndex, int pageSize, out int totalCount);
        bool CreateANewCategory(string userId, String categoryName);
    }
}
