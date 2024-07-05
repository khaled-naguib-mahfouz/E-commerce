namespace E_commerce.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int id);

        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> DeleteCartItemAsync(int id);
    }
}
