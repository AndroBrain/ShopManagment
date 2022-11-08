using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface IBillingInfoRepository
    {
        public BillingInfo CreateBillingInfo(BillingInfo billingInfo);
        public BillingInfo GetBillingInfoById(int id);
        public BillingInfo AddCard(int id, Card card);
        public BillingInfo DeleteBillingInfo(int id);
    }
}
