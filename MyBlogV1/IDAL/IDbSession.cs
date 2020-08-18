using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDbSession
    {
        DbContext dbContext { get; }

        bool SaveChanges();

        IArticleDAL ArticleDAL { get; set; }
        IArticleCategoryIntDAL ArticleCategoryIntDAL { get; set; }
        ICategoryDAL CategoryDAL { get; set; }
        ICommentDAL CommentDAL { get; set; }
        IFavoriteDAL FavoriteDAL { get; set; }
        IFollowingDAL FollowingDAL { get; set; }
        IReplyDAL ReplyDAL { get; set; }
        IRoleDAL RoleDAL { get; set; }
        IUserDAL UserDAL { get; set; }
        IUserRoleIntDAL UserRoleIntDAL { get; set; }

    }
}
