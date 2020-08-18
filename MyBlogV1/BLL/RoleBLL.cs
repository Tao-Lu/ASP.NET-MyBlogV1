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
    public class RoleBLL : BaseBLL<Role>, IRoleBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.RoleDAL;
        }

        public List<User> GetRoleUsers(string roleId)
        {
            List<User> users = new List<User>();

            foreach (UserRoleInt userRoleInt in dbSession.UserRoleIntDAL.GetEntities(m => !m.IsRemoved && m.RoleId == roleId))
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == userRoleInt.UserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        
    }
}
