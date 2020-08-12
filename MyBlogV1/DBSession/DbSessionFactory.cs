using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DBSession
{
    /// <summary>
    /// singleton
    /// create a single dbSession
    /// </summary>
    public class DbSessionFactory
    {
        private static DbSession _dbSession;

        public static DbSession CreateDbSession()
        {
            _dbSession = (DbSession)CallContext.GetData("DbSession");
            if (_dbSession == null)
            {
                _dbSession = new DbSession();
                CallContext.SetData("DbSession", _dbSession);
            }
            
            return _dbSession;
        }
    }
}
