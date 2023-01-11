namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartDetailVO
    {
        public long Id { set; get; }
        public long CartHeaderId { get; set; }
        public CartHeaderVO CartHeader { get; set; }
        public long ProductId { get; set; }
        public ProductVO Product { get; set; }
        public long Count { get; set; }
    }
}
