namespace Maui_Shopping_APP.Messages
{
    public class CartBadgeMessage
    {
        public int ItemCount { get; }

        public CartBadgeMessage(int itemCount)
        {
            ItemCount = itemCount;
        }
    }
}
