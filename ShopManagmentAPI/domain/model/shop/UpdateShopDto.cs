namespace ShopManagmentAPI.domain.model.shop
{
    public class UpdateShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShopType Type { get; set; }
    }
}
