using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBSession
{
    public class AbstractFactory
    {
        private static readonly string AssessmblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        
        public static object CreateInstance(string fullClassName)
        {
            return Assembly.Load(AssessmblyPath).CreateInstance(fullClassName);
        }

        // create DALs
        public static IArticleDAL CreateArticleDAL()
        {
            string fullClassName = AssessmblyPath + ".ArticleDAL";
            
            return CreateInstance(fullClassName) as IArticleDAL;
        }

        public static IArticleCategoryIntDAL CreateArticleCategoryIntDAL()
        {
            string fullClassName = AssessmblyPath + ".ArticleCategoryIntDAL";

            return CreateInstance(fullClassName) as IArticleCategoryIntDAL;
        }

        public static ICategoryDAL CreateCategoryDAL()
        {
            string fullClassName = AssessmblyPath + ".CategoryDAL";

            return CreateInstance(fullClassName) as ICategoryDAL;
        }

        public static ICommentDAL CreateCommentDAL()
        {
            string fullClassName = AssessmblyPath + ".CommentDAL";

            return CreateInstance(fullClassName) as ICommentDAL;
        }

        public static IFavoriteDAL CreateFavoriteDAL()
        {
            string fullClassName = AssessmblyPath + ".FavoriteDAL";

            return CreateInstance(fullClassName) as IFavoriteDAL;
        }

        public static IFollowingDAL CreateFollowingDAL()
        {
            string fullClassName = AssessmblyPath + ".FollowingDAL";

            return CreateInstance(fullClassName) as IFollowingDAL;
        }

        public static IReplyDAL CreateReplyDAL()
        {
            string fullClassName = AssessmblyPath + ".ReplyDAL";

            return CreateInstance(fullClassName) as IReplyDAL;
        }

        public static IRoleDAL CreateRoleDAL()
        {
            string fullClassName = AssessmblyPath + ".RoleDAL";

            return CreateInstance(fullClassName) as IRoleDAL;
        }

        public static IUserDAL CreateUserDAL()
        {
            string fullClassName = AssessmblyPath + ".UserDAL";

            return CreateInstance(fullClassName) as IUserDAL;
        }

        public static IUserRoleIntDAL CreateUserRoleIntDAL()
        {
            string fullClassName = AssessmblyPath + ".UserRoleIntDAL";

            return CreateInstance(fullClassName) as IUserRoleIntDAL;
        }
    }
}
