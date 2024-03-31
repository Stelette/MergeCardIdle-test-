using Implementation.Items;

namespace Implementation.Providers
{
    public interface ICardProvider
    {
        void Init();
        Item GetRandomCard();
        Item GetPlayerCard();
    }
}