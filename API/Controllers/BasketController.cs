using API.Data;
using API.DTOs;
using API.Entities;
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
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();

            return MapBasketToDto(basket);

        }

        [HttpPost] // api/basket?productId=3&quantity=2
        public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
        {
            // Get Basket || Create Basket
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();

            // Get Product
            var product = await _context.Products.FindAsync(productId);
            if(product == null) return NotFound();

            // Add Item
            basket.AddItem(product, quantity);

            // save Changes
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));
            return BadRequest(new ProblemDetails { Title = "Proplem saving item to basket.!" });

        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // Get Item
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();

            // Remove or Reduce Item
            basket.RemoveItem(productId, quantity);

            // Save Changes
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }
        private async Task<Basket> RetrieveBasket()
        {
            var result =  await _context.Baskets.Include(i => i.Items)
               .ThenInclude(p => p.Product)
               .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);

            return result;
        }
        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);

            var basket = new Basket { BuyerId = buyerId};
            _context.Baskets.Add(basket);

            return basket;
        }
        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                BasketItemDtos = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

    }
}
