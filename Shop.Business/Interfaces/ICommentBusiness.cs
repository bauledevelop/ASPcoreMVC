﻿using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface ICommentBusiness
    {
        void DeleteComment(long id);
        long GetTotal();
        IEnumerable<CommentDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<CommentDTO> SelectAll();
    }
}
