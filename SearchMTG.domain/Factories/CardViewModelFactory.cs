using SearchMTG.domain.Cards.Models;
using SearchMTG.domain.ViewModels;
using System.Linq;

namespace SearchMTG.domain.Factories
{
    public static class CardViewModelFactory
    {
        public static CardViewModel GetCardViewModel(CardInfo cardInfo)
        {
            return new CardViewModel()
            {
                Id = cardInfo.Id,
                Name = cardInfo.Name,
                Rarity = cardInfo.CardRarity.First().CardRarity.Id,
                SetName = cardInfo.CardSet.First().CardSet.Id,
                ManaCost = cardInfo.ManaCost,
                CMC = cardInfo.ConvertedManaCost,
                Type = cardInfo.TypeName,
                Text = cardInfo.Text,
                Flavor = cardInfo.Flavor,
                Power = cardInfo.Power,
                Toughness = cardInfo.Toughness,
                Artist = cardInfo.Artist,
                NormalImage = cardInfo.NormalImage,
                CroppedImage = cardInfo.CroppedImage,
            };
        }
    }
}
