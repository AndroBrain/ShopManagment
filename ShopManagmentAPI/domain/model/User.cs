using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.domain.model
{
    public class User
    {
        public string Name { get; set; }
        public List<ShopEntity> Shops { get; set; }
        public virtual BillingInfoEntity BillingInfo { get; set; }
    }
}
