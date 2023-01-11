namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartHeaderVO
    {
        public long Id { set; get; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
