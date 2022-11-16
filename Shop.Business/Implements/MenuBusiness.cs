using AutoMapper;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using Shop.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class MenuBusiness : IMenuBusiness
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        public MenuBusiness(IMenuRepository menuRepository,IMapper mapper, ICategoryProductBusiness categoryProductBusiness)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
            _categoryProductBusiness = categoryProductBusiness;
        }
        public IEnumerable<MenuDTO> SelectAll()
        {
            var menus = _menuRepository.SelectAllByDelete();
            var menuDTOs = menus.Select(item => _mapper.Map<Menu, MenuDTO>(item));
            return menuDTOs;
        }
        public MenuDTO GetMenuById(long id)
        {
            var menu = _menuRepository.SelectById(id);
            var menuDTO = _mapper.Map<Menu, MenuDTO>(menu);
            return menuDTO;
        }
        public void DeleteMenu(long id)
        {
            _categoryProductBusiness.DeleteByMenuID(id);
            _menuRepository.Delete(id);
            _menuRepository.Save();
        }
        public void EditMenu(MenuDTO menuDTO)
        {
            var menu = _mapper.Map<MenuDTO, Menu>(menuDTO);
            menu.UpdatedDate = DateTime.Now;
            _menuRepository.Update(menu);
            _menuRepository.Save();
        }
        public void InsertMenu(MenuDTO menuDTO)
        {
            var menu = _mapper.Map<MenuDTO, Menu>(menuDTO);
            menu.CreatedDate = DateTime.Now;
            menu.CreatedBy = 1;
            menu.UpdatedDate = DateTime.Now;
            menu.IsDelete = false;
            _menuRepository.Insert(menu);
            _menuRepository.Save();
        }
        public MenuDTO SelectById(long id)
        {
            var menu = _menuRepository.SelectById(id);
            var menuDto = _mapper.Map<Menu, MenuDTO>(menu);
            return menuDto;
        }
        public IEnumerable<MenuDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var menus = _menuRepository.SelectByQuantityItem(page, pageSize);
            var menuDtos = menus.Select(item => _mapper.Map<Menu, MenuDTO>(item));
            return menuDtos;
        }
        public long GetTotal()
        {
            return _menuRepository.GetTotal();
        }
    }
}
