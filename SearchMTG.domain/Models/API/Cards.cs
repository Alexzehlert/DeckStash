using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchMTG.domain.Models.API
{
    public class Cards
    {
        [JsonProperty(PropertyName = "has_more")]
        public bool HasMore { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Card> CardList { get; set; }
    }

    public class Card
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "lang")]
        public string Language { get; set; }
        //[JsonProperty(PropertyName = "multiverse_ids")]
        //public List<int> MultiverseIds { get; set; }
        [JsonProperty(PropertyName = "image_uris")]
        public CardImageURIs ImageURIs { get; set; }
        [JsonProperty(PropertyName = "released_at")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "rarity")]
        public string Rarity { get; set; }
        [JsonProperty(PropertyName = "set")]
        public string Set { get; set; }
        [JsonProperty(PropertyName = "set_name")]
        public string SetName { get; set; }
        [JsonProperty(PropertyName = "mana_cost")]
        public string ManaCost { get; set; }
        [JsonProperty(PropertyName = "cmc")]
        public float ConvertedManaCost { get; set; }
        [JsonProperty(PropertyName = "colors")]
        public List<string> Colors { get; set; }
        [JsonProperty(PropertyName = "type_line")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "oracle_text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "flavor_text")]
        public string Flavor { get; set; }
        [JsonProperty(PropertyName = "power")]
        public string Power { get; set; }
        [JsonProperty(PropertyName = "toughness")]
        public string Toughness { get; set; }
        [JsonProperty(PropertyName = "usd")]
        public string Price { get; set; }
        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }
    }

    public class CardImageURIs
    {
        [JsonProperty(PropertyName = "normal")]
        public string Normal { get; set; }
        [JsonProperty(PropertyName = "art_crop")]
        public string ArtCrop { get; set; }
    }
}
