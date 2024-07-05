namespace E_commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }
}
