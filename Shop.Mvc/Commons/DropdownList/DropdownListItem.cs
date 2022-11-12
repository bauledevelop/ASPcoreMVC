using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Commons.DropdownList
{
    public class DropdownListItem
    {
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
