using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Cart>> GetCartByUserId(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            var createdCart = await _cartService.CreateCartAsync(cart);
            return CreatedAtAction(nameof(GetCartById), new { id = createdCart.Id }, createdCart);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cart>> UpdateCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }
            await _cartService.UpdateCartAsync(cart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCart(int id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // CartItem operations
        [HttpPost("{cartId}/items")]
        public async Task<ActionResult<CartItem>> AddCartItem(int cartId, CartItem cartItem)
        {
            cartItem.CartId = cartId;
            var createdCartItem = await _cartService.AddCartItemAsync(cartItem);
            return CreatedAtAction(nameof(GetCartById), new { id = cartId }, createdCartItem);
        }

        [HttpPut("{cartId}/items/{id}")]
        public async Task<ActionResult<CartItem>> UpdateCartItem(int cartId, int id, CartItem cartItem)
        {
            if (id != cartItem.Id || cartId != cartItem.CartId)
            {
                return BadRequest();
            }
            await _cartService.UpdateCartItemAsync(cartItem);
            return NoContent();
        }

        [HttpDelete("{cartId}/items/{id}")]
        public async Task<ActionResult> DeleteCartItem(int cartId, int id)
        {
            var result = await _cartService.DeleteCartItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
