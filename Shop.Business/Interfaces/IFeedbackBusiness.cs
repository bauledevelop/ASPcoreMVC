using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IFeedbackBusiness
    {
        void DeleteFeedback(long id);
        long GetTotal();
        IEnumerable<FeedbackDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<FeedbackDTO> SelectAll();
    }
}
