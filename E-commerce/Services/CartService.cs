namespace E_commerce.Services
{
    public class CartService:ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartService(IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _cartRepository.GetAllAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _cartRepository.FindAsync(c => c.UserId == userId);
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _cartRepository.CreateAsync(cart);
            return cart;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            await _cartRepository.UpdateAsync(cart);
            return cart;
        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            return await _cartRepository.DeleteAsync(id);
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.CreateAsync(cartItem);
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.UpdateAsync(cartItem);
            return cartItem;
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            return await _cartItemRepository.DeleteAsync(id);
        }
    }
}
