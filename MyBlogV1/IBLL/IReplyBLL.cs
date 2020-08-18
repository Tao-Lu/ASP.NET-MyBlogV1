using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IReplyBLL: IBaseBLL<Reply>
    {
        User GetReplyUser(string replyId);
        User GetReplyToUser(string replyId);
    }
}
