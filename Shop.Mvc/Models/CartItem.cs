using Shop.Common.DTO;

namespace Shop.Mvc.Models
{
    public class CartItem
    {
        public ProductDTO Product { set; get; }
        public string Size { set; get; }
        public string Color { set; get; }
        public int Amount { set; get; }
        public long TotalMoney => Amount * Product.Price;
    }
}
