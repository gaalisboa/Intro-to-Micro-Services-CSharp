namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class ProductVO
    {
        public long Id { set; get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}