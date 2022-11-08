using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface ICardRepository
    {
        public Card AddCard(Card card);
        public Card GetCardById(int id);
        public Card UpdateCard(Card card);
        public Card DeleteCard(int id);
    }
}
