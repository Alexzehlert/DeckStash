using SearchMTG.domain.Cards.Models;
using SearchMTG.domain.Models.API;

namespace SearchMTG.domain.Factories
{
    public static class CardInfoFactory
    {
        public static CardInfo GetCardInfo(Card card)
        {
            return new CardInfo()
            {
                CardId = card.Id,
                ReleaseDate = card.ReleaseDate,
                Name = card.Name,
                ManaCost = card.ManaCost,
                ConvertedManaCost = card.ConvertedManaCost,
                TypeName = card.Type,
                Text = card.Text,
                Flavor = card.Flavor,
                Power = card.Power,
                Toughness = card.Toughness,
                Price = double.TryParse(card.Price, out double price) ? price : 0.0,
                Artist = card.Artist,
                NormalImage = card.ImageURIs.Normal,
                CroppedImage = card.ImageURIs.ArtCrop,
            };
        }
    }
}
