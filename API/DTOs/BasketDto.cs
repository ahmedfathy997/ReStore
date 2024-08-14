namespace API.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List <BasketItemDto> BasketItemDtos{ get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }


    }
}
