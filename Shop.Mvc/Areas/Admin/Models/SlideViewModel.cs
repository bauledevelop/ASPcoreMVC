using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class SlideViewModel
    {
        [Display(Name = "FileContent")]
        public string Content { set; get; }
       
    }
}
