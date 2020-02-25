using SearchMTG.domain.Cards.Models;
using SearchMTG.domain.Db;
using SearchMTG.domain.Factories;
using SearchMTG.domain.Models.API;
using SearchMTG.domain.Models.Cards.Relations;
using SearchMTG.domain.Util;
using SearchMTG.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SearchMTG.Controllers
{
    public class AdminController : Controller
    {
        public IMainContext context;

        private static string[] typeDelimiters = new string[] { " ", "//" };

        public AdminController(IMainContext context)
        {
            this.context = context;
        }

        private bool ValidateSyncKey(string syncKey)
        {
            var appSyncKey = WebConfigurationManager.AppSettings["SyncKey"];
            return (appSyncKey == syncKey);
        }

        [HttpPost]
        [ActionName("sync-cards")]
        public async Task<ActionResult> SyncCards(string syncKey)
        {
            try
            {
                if (ValidateSyncKey(syncKey) == false)
                    return Json("Unauthorized");

                var cardRarities = context.Select<CardRarity>();
                var localColors = new Dictionary<string, Color>();
                var localCardSets = new Dictionary<string, CardSet>();
                var localCardRarities = new Dictionary<string, CardRarity>();
                var localCardTypes = new Dictionary<string, CardType>();
                var localCardSubTypes = new Dictionary<string, CardSubType>();
                
                // Fetch first page of cards
                var url = Paths.GetScryCardsURL(1);
                var cardsPageTask = Common.HttpGet<Cards>(url);
                for (var i = 2; /*i < 200*/; i += 1)
                {
                    var cardsPage = await cardsPageTask;
                    // Check for next page and setup task to grab next page
                    if (cardsPage.HasMore) {
                        url = Paths.GetScryCardsURL(i);
                        cardsPageTask = Common.HttpGet<Cards>(url);
                    }
                    var cardInfos = new List<CardInfo>();
                    var colorRelations = new List<CardColorRelation>();
                    var cardSetRelations = new List<CardSetRelation>();
                    var cardRarityRelations = new List<CardRarityRelation>();
                    var cardTypeRelations = new List<CardTypeRelation>();
                    var cardSubTypeRelations = new List<CardSubTypeRelation>();
                    foreach (var card in cardsPage.CardList)
                    {
                        // Check if card is english
                        if (card.Language != "en")
                            continue;
                        // Check if card image is null (ignore it)
                        if (card.ImageURIs == null)
                            continue;

                        // Check if card is already in Db using CardId
                        var cardFound = context.Select<CardInfo>()
                            .Any(c => c.CardId == card.Id);
                            //.FirstOrDefault(c => c.CardId == card.Id);
                        if (cardFound == true)
                            continue;

                        // Restrict some outliers cards
                        if (card.Name.Length > 60)
                            continue;

                        // Create card index
                        var cardInfo = CardInfoFactory.GetCardInfo(card);
                        // Add colors of card
                        foreach (var cardColor in card.Colors)
                        {
                            // Check local
                            Color color = null;
                            if (localColors.ContainsKey(cardColor))
                                color = localColors[cardColor];
                            // Check in Db
                            if (color == null)
                                color = context.Select<Color>().FirstOrDefault(c => c.Abbv == cardColor);
                            // Create new color
                            if (color == null) {
                                color = new Color() { Name = GetColorName(cardColor), Abbv = cardColor };
                                context.Select<Color>().Add(color);
                                localColors.Add(cardColor, color);
                            }
                            colorRelations.Add(new CardColorRelation() { Card = cardInfo, Color = color });
                        }

                        // Parse type and subtypes
                        var splitResult = card.Type.Split('—');

                        // Add types of card
                        if (splitResult.Length > 0)
                        {
                            var cardTypeSplit = splitResult[0].Trim().Split(typeDelimiters, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var cardType in cardTypeSplit) {
                                // Check local
                                CardType cardTypeObject = null;
                                if (localCardTypes.ContainsKey(cardType))
                                    cardTypeObject = localCardTypes[cardType];
                                // Check in Db
                                if (cardTypeObject == null)
                                    cardTypeObject = context.Select<CardType>().FirstOrDefault(ct => ct.Name == cardType);
                                // Create new type
                                if (cardTypeObject == null) {
                                    cardTypeObject = new CardType() { Name = cardType };
                                    context.Select<CardType>().Add(cardTypeObject);
                                    localCardTypes.Add(cardType, cardTypeObject);
                                }
                                cardTypeRelations.Add(new CardTypeRelation() { Card = cardInfo, Type = cardTypeObject });
                            }
                        }

                        // Add sub-types of card
                        if (splitResult.Length > 1)
                        {
                            var cardSubTypesSplit = splitResult[1].Trim().Split(typeDelimiters, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var cardSubType in cardSubTypesSplit) {
                                // Check local
                                CardSubType cardSubTypeObject = null;
                                if (localCardSubTypes.ContainsKey(cardSubType))
                                    cardSubTypeObject = localCardSubTypes[cardSubType];
                                // Check in Db
                                if (cardSubTypeObject == null)
                                    cardSubTypeObject = context.Select<CardSubType>().FirstOrDefault(cst => cst.Name == cardSubType);
                                // Create new sub type
                                if (cardSubTypeObject == null) {
                                    cardSubTypeObject = new CardSubType() { Name = cardSubType };
                                    context.Select<CardSubType>().Add(cardSubTypeObject);
                                    localCardSubTypes.Add(cardSubType, cardSubTypeObject);
                                }
                                cardSubTypeRelations.Add(new CardSubTypeRelation() { Card = cardInfo, SubType = cardSubTypeObject });
                            }
                        }

                        // Add card set
                        // Check local
                        CardSet cardSetObject = null;
                        if (localCardSets.ContainsKey(card.Set))
                            cardSetObject = localCardSets[card.Set];
                        // Check in Db
                        if (cardSetObject == null)
                            cardSetObject = context.Select<CardSet>().FirstOrDefault(ct => ct.Abbv == card.Set);
                        // Create new set
                        if (cardSetObject == null) {
                            cardSetObject = new CardSet() { Name = card.SetName, Abbv = card.Set };
                            context.Select<CardSet>().Add(cardSetObject);
                            localCardSets.Add(card.Set, cardSetObject);
                        }
                        cardSetRelations.Add(new CardSetRelation() { Card = cardInfo, CardSet = cardSetObject });
                        
                        // Add card rarity
                        // Check local
                        CardRarity cardRarityObject = null;
                        if (localCardRarities.ContainsKey(card.Rarity))
                            cardRarityObject = localCardRarities[card.Rarity];
                        // Check in Db
                        if (cardRarityObject == null)
                            cardRarityObject = context.Select<CardRarity>().FirstOrDefault(cr => cr.Name == card.Rarity);
                        // Create new set
                        if (cardRarityObject == null)
                        {
                            cardRarityObject = new CardRarity() { Name = card.Rarity };
                            context.Select<CardRarity>().Add(cardRarityObject);
                            localCardRarities.Add(card.Rarity, cardRarityObject);
                        }
                        cardRarityRelations.Add(new CardRarityRelation() { Card = cardInfo, CardRarity = cardRarityObject });

                        // Add card info
                        cardInfos.Add(cardInfo);
                    }
                    // Add cards to Db
                    context.Select<CardInfo>().AddRange(cardInfos);
                    context.Select<CardColorRelation>().AddRange(colorRelations);
                    context.Select<CardTypeRelation>().AddRange(cardTypeRelations);
                    context.Select<CardSubTypeRelation>().AddRange(cardSubTypeRelations);
                    context.Select<CardSetRelation>().AddRange(cardSetRelations);
                    context.Select<CardRarityRelation>().AddRange(cardRarityRelations);
                    // Commit page
                    context.Commit();
                    Thread.Sleep(100);

                    // Check if all done
                    if (cardsPage.HasMore == false)
                        break;
                }

                context.Dispose();
                return Json("success");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(ex.Message);
            }
        }

        private static string GetColorName(string colorChar)
        {
            switch (colorChar)
            {
                case "R": return "Red";
                case "G": return "Green";
                case "B": return "Black";
                case "U": return "Blue";
                case "W": return "White";
                default: return colorChar;
            }
        }
    }
}