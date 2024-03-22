namespace Mango.Services.ShoppingCartAPI.Models.DTO
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; }
        public IEnumerable<CartDetialsDTO>? CartDetails { get; set; }
    }
}

