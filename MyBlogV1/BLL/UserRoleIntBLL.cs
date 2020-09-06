using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserRoleIntBLL : BaseBLL<UserRoleInt>, IUserRoleIntBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.UserRoleIntDAL;
        }
    }
}
