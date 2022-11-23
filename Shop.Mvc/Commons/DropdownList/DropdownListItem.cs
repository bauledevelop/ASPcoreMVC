using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Commons.DropdownList
{
    public class DropdownListItem
    {
        public IEnumerable<SelectListItem> DropdownListMenuActive(IEnumerable<MenuDTO> menuDTOs,long active)
        {
            var listItem = new List<SelectListItem>();
            var target = menuDTOs.SingleOrDefault(item => item.ID == active);
            listItem.Add(new SelectListItem() { Text = target.Name, Value = target.ID.ToString() });
            foreach (var item in menuDTOs)
            {
                if (target.ID != item.ID)
                {
                    listItem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
                }
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListMenu(IEnumerable<MenuDTO> menuDTOs)
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Chọn Menu", Value = "0" });
            foreach(var item in menuDTOs)
            {
                listItem.Add(new SelectListItem { Text = item.Name ,Value = item.ID.ToString()});
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListProductActive(IEnumerable<ProductDTO> productDTOs,long active)
        {
            var listItem = new List<SelectListItem>();
            var target = productDTOs.SingleOrDefault(item => item.ID == active);
            listItem.Add(new SelectListItem() { Text = target.Name, Value = target.ID.ToString() });
            foreach (var item in productDTOs)
            {
                if (target.ID != item.ID)
                    listItem.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListProduct(IEnumerable<ProductDTO> productDTOs)
        {
            var listItem = new List<SelectListItem>();
            foreach (var item in productDTOs)
            {
                listItem.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListTypeFileActive(string active)
        {
            var listItem = new List<SelectListItem>();
            if (active == "1")
            {
                listItem.Add(new SelectListItem() { Text = "Hình ảnh", Value = "1" });
                listItem.Add(new SelectListItem() { Text = "Video", Value = "2" });
            }
            else
            {
                listItem.Add(new SelectListItem() { Text = "Video", Value = "2" });
                listItem.Add(new SelectListItem() { Text = "Hình ảnh", Value = "1" });
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListTypeFile()
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Hình ảnh", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "Video", Value = "2" });
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListCategoryActive(IEnumerable<CategoryProductDTO> categoryProductDTO,long active)
        {
            var listItem = new List<SelectListItem>();
            var target = categoryProductDTO.SingleOrDefault(item => item.ID == active);
            listItem.Add(new SelectListItem() { Text = target.Name, Value = target.ID.ToString()});
            foreach (var item in categoryProductDTO)
            {
                if (target.ID != item.ID)
                    listItem.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListCategory(IEnumerable<CategoryProductDTO> categoryProductDTO)
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Chọn loại sản phẩm", Value = "0" });
            foreach (var item in categoryProductDTO)
            {
                listItem.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListTypeSexActive(string active)
        {
            var listItem = new List<SelectListItem>();
            switch (active)
            {
                case "1":
                    listItem.Add(new SelectListItem() { Text = "Nam", Value = "1" });
                    listItem.Add(new SelectListItem() { Text = "Nữ", Value = "2" });
                    listItem.Add(new SelectListItem() { Text = "Khác", Value = "3" });
                    break;
                case "2":
                    listItem.Add(new SelectListItem() { Text = "Nữ", Value = "2" });
                    listItem.Add(new SelectListItem() { Text = "Nam", Value = "1" });
                    listItem.Add(new SelectListItem() { Text = "Khác", Value = "3" });
                    break;
                case "3":
                    listItem.Add(new SelectListItem() { Text = "Khác", Value = "3" });
                    listItem.Add(new SelectListItem() { Text = "Nam", Value = "1" });
                    listItem.Add(new SelectListItem() { Text = "Nữ", Value = "2" });
                    break;
            }
            return listItem;
               
        }
        public IEnumerable<SelectListItem> DropdownListTypeAccountActive(string active)
        {
            var listItem = new List<SelectListItem>();
            switch (active){
                case "1":
                    listItem.Add(new SelectListItem() { Text = "Quản lý", Value = "1" });
                    listItem.Add(new SelectListItem() { Text = "Người dùng", Value = "2" });
                    break;
                case "2":
                    listItem.Add(new SelectListItem() { Text = "Người dùng", Value = "2" });
                    listItem.Add(new SelectListItem() { Text = "Quản lý", Value = "1" });
                    break;
            }
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListTypeSex()
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Nam", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "Nữ", Value = "2" });
            listItem.Add(new SelectListItem() { Text = "Khác", Value = "3" });
            return listItem;
        }
        public IEnumerable<SelectListItem> DropdownListTypeAccount()
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Quản lý", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "Người dùng", Value = "2" });
            return listItem;
        }
    }
}
