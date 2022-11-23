using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IMenuBusiness
    {
        IEnumerable<MenuDTO> SelectAllByStatus();
        IEnumerable<MenuDTO> SelectAll();
        MenuDTO GetMenuById(long id);
        void DeleteMenu(long id);
        void EditMenu(MenuDTO menuDTO);
        void InsertMenu(MenuDTO menuDTO);
        MenuDTO SelectById(long id);
        IEnumerable<MenuDTO> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
    }
}
