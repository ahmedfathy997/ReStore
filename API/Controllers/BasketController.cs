using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;

        public BasketController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet(Name="GetBasket")] // api/basket
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();

            return basket.MapBasketToDto();

        }

        [HttpPost] // api/basket?productId=3&quantity=2
        public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
        {
            // Get Basket || Create Basket
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket = CreateBasket();

            // Get Product
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return BadRequest(new ProblemDetails { Title = "Product Not Found" });

            // Add Item
            basket.AddItem(product, quantity);

            // save Changes
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());
            return BadRequest(new ProblemDetails { Title = "Proplem saving item to basket.!" });

        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // Get Item
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();

            // Remove or Reduce Item
            basket.RemoveItem(productId, quantity);

            // Save Changes
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }
        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            var result =  await _context.Baskets.Include(i => i.Items)
               .ThenInclude(p => p.Product)
               .FirstOrDefaultAsync(x => x.BuyerId == buyerId);

            return result;
        }

        private string GetBuyerId()
        {
            return User.Identity.Name ?? Request.Cookies[("buyerId")];
        }

        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }

            var basket = new Basket { BuyerId = buyerId};
            _context.Baskets.Add(basket);

            return basket;
        }
    }
}
