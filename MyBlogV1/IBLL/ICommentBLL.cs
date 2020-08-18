using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICommentBLL: IBaseBLL<Comment>
    {
        User GetCommentUser(string commentId);
        List<Reply> GetCommentReplies(string commentId);
    }
}
