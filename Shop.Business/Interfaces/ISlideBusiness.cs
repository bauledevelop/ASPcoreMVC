using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface ISlideBusiness
    {
        void InsertSlide(SlideDTO slideDTO);
        void DeleteSlide(long id);
        IEnumerable<SlideDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<SlideDTO> SelectAll();
    }
}
