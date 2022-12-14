using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IRateBusiness
    {
        IEnumerable<RateDTO> SelectByIDProduct(long IDProduct);
        void Insert(RateDTO rateDTO);
        IEnumerable<RateDTO> GetRateByIDAccount(long idAccount);
    }
}
