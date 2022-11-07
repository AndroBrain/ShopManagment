namespace ShopManagmentAPI.data.entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShopEntity> Shops { get; set; }
        public virtual BillingInfoEntity BillingInfo { get; set; }
    }
}
