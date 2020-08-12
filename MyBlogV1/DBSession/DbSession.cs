using DAL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSession
{
    public class DbSession
    {
        private DbContext _dbContext = DbContextFactory.CreateDbContext();
        
        // unit of work
        public bool saveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }

        // using AbstractFactory to create DAL object
        private IArticleDAL _ArticleDAL;
        public IArticleDAL ArticleDAL
        {
            get
            {
                if(_ArticleDAL == null)
                {
                    _ArticleDAL = AbstractFactory.CreateArticleDAL();
                }
                return _ArticleDAL;
            }
            set
            {
                _ArticleDAL = value;
            }
        }

        private IArticleCategoryIntDAL  _ArticleCategoryIntDAL;
        public IArticleCategoryIntDAL ArticleCategoryIntDAL
        {
            get
            {
                if (_ArticleCategoryIntDAL == null)
                {
                    _ArticleCategoryIntDAL = AbstractFactory.CreateArticleCategoryIntDAL();
                }
                return _ArticleCategoryIntDAL;
            }
            set
            {
                _ArticleCategoryIntDAL = value;
            }
        }

        private ICategoryDAL _CategoryDAL;
        public ICategoryDAL CategoryDAL
        {
            get
            {
                if (_CategoryDAL == null)
                {
                    _CategoryDAL = AbstractFactory.CreateCategoryDAL();
                }
                return _CategoryDAL;
            }
            set
            {
                _CategoryDAL = value;
            }
        }

        private ICommentDAL _CommentDAL;
        public ICommentDAL CommentDAL
        {
            get
            {
                if (_CommentDAL == null)
                {
                    _CommentDAL = AbstractFactory.CreateCommentDAL();
                }
                return _CommentDAL;
            }
            set
            {
                _CommentDAL = value;
            }
        }

        private IFavoriteDAL _FavoriteDAL;
        public IFavoriteDAL FavoriteDAL
        {
            get
            {
                if (_FavoriteDAL == null)
                {
                    _FavoriteDAL = AbstractFactory.CreateFavoriteDAL();
                }
                return _FavoriteDAL;
            }
            set
            {
                _FavoriteDAL = value;
            }
        }

        private IFollowingDAL _FollowingDAL;
        public IFollowingDAL FollowingDAL
        {
            get
            {
                if (_FollowingDAL == null)
                {
                    _FollowingDAL = AbstractFactory.CreateFollowingDAL();
                }
                return _FollowingDAL;
            }
            set
            {
                _FollowingDAL = value;
            }
        }

        private IReplyDAL _ReplyDAL;
        public IReplyDAL ReplyDAL
        {
            get
            {
                if (_ReplyDAL == null)
                {
                    _ReplyDAL = AbstractFactory.CreateReplyDAL();
                }
                return _ReplyDAL;
            }
            set
            {
                _ReplyDAL = value;
            }
        }

        private IRoleDAL _RoleDAL;
        public IRoleDAL RoleDAL
        {
            get
            {
                if (_RoleDAL == null)
                {
                    _RoleDAL = AbstractFactory.CreateRoleDAL();
                }
                return _RoleDAL;
            }
            set
            {
                _RoleDAL = value;
            }
        }

        private IUserDAL _UserDAL;
        public IUserDAL UserDAL
        {
            get
            {
                if (_UserDAL == null)
                {
                    _UserDAL = AbstractFactory.CreateUserDAL();
                }
                return _UserDAL;
            }
            set
            {
                _UserDAL = value;
            }
        }

        private IUserRoleIntDAL _UserRoleIntDAL;
        public IUserRoleIntDAL UserRoleIntDAL
        {
            get
            {
                if (_UserRoleIntDAL == null)
                {
                    _UserRoleIntDAL = AbstractFactory.CreateUserRoleIntDAL();
                }
                return _UserRoleIntDAL;
            }
            set
            {
                _UserRoleIntDAL = value;
            }
        }
    }
}
