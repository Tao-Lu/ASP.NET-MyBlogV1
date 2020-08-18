using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReplyBLL : BaseBLL<Reply>, IReplyBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.ReplyDAL;
        }

        public User GetReplyUser(string replyId)
        {
            Reply reply = dbSession.ReplyDAL.GetEntity(m => !m.IsRemoved && m.Id == replyId);
            if(reply != null)
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == reply.UserId);
                if(user != null)
                {
                    return user;
                }
            }

            return null;
        }

        public User GetReplyToUser(string replyId)
        {
            Reply reply = dbSession.ReplyDAL.GetEntity(m => !m.IsRemoved && m.Id == replyId);
            if (reply != null)
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == reply.ReplyToUserId);
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
