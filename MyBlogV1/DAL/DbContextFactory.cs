using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// singleton
    /// create a single dbContext
    /// </summary>
    public class DbContextFactory
    {
        private static DbContext _dbContext;

        public static DbContext CreateDbContext()
        {
            _dbContext = (DbContext) CallContext.GetData("DbContext");
            if(_dbContext == null)
            {
                _dbContext = new MyBlogV1DBEntities();
                CallContext.SetData("DbContext", _dbContext);
            }
            
            return _dbContext;
        }
    }
}
