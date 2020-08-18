using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentBLL : BaseBLL<Comment>, ICommentBLL
    {
        public override void SetBaseDAL()
        {
            baseDAL = dbSession.CommentDAL;
        }

        public List<Reply> GetCommentReplies(string commentId)
        {
            List<Reply> replies = new List<Reply>();

            foreach (Reply reply in dbSession.ReplyDAL.GetEntities(m => !m.IsRemoved && m.CommentId == commentId))
            {
                replies.Add(reply);
            }

            return replies;
        }

        public User GetCommentUser(string commentId)
        {
            Comment comment = dbSession.CommentDAL.GetEntity(m => !m.IsRemoved && m.Id == commentId);
            if(comment != null)
            {
                User user = dbSession.UserDAL.GetEntity(m => !m.IsRemoved && m.Id == comment.UserId);
                if(user != null)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
