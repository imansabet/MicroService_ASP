namespace Mango.Web.Models
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; }
        public IEnumerable<CartDetialsDTO>? CartDetails { get; set; }
    }
}

