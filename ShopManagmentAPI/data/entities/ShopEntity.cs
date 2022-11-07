namespace ShopManagmentAPI.data.entities
{
    public class ShopEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductEntity> Products { get; set; }
    }
}
