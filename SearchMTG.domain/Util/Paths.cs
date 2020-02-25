using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchMTG.Util
{
    public static class Paths
    {
        private static readonly string MTGCardsFormat = "https://api.magicthegathering.io/v1/cards?page={0}";


        private static readonly string ScryFallBase = "https://api.scryfall.com";

        private static readonly string ScryCardsFormat = "{0}/cards?page={1}";
        private static readonly string ScryCardFormat = "{0}/cards/multiverse/{1}";

        public static string GetMTGCardsURL(int page)
        {
            return string.Format(MTGCardsFormat, page);
        }
        
        public static string GetScryCardsURL(int page)
        {
            return string.Format(ScryCardsFormat, ScryFallBase, page);
        }

        public static string GetScryCardURL(int multiverseId)
        {
            return string.Format(ScryCardFormat, ScryFallBase, multiverseId);
        }
    }
}