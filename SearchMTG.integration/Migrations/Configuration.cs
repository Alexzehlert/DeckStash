namespace SearchMTG.integration.Migrations
{
    using SearchMTG.domain.Db;
    using SearchMTG.domain.Factories;
    using SearchMTG.domain.Models;
    using SearchMTG.domain.Models.API;
    using SearchMTG.domain.Util;
    using SearchMTG.Util;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        // 1. Enable-Migrations -ContextProjectName SearchMTG.domain -ContextTypeName MainContext -ProjectName SearchMTG.integration -StartUpProjectName SearchMTG.web

        // 2. Add-Migration InitialCreate -ProjectName SearchMTG.integration

        // 2. Update-Database -ProjectName SearchMTG.integration

        //Update-Database [-SourceMigration <String>] [-TargetMigration <String>] [-Script] [-Force] 
        //[-ProjectName<String>]
        //[-StartUpProjectName<String>]
        //[-ConfigurationTypeName<String>]
        //[-ConnectionStringName<String>]
        //[-AppDomainBaseDirectory<String>]
        //[<CommonParameters>]

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(MainContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //try
            //{
            //    if (System.Diagnostics.Debugger.IsAttached == false)
            //        System.Diagnostics.Debugger.Launch();

            //    var cardIndices = context.Select<CardIndex>();
            //    var colorRelations = context.Select<ColorRelation>();
            //    var colors = context.Select<Color>();
            //    var cardTypeRelations = context.Select<CardTypeRelation>();
            //    var cardTypes = context.Select<CardType>();
            //    for (var i = 0; i < 10; i += 1)
            //    {
            //        var url = Paths.GetCardsURL(i);
            //        var cardsPage = await Common.HttpGet<Cards>(url);
            //        var localColors = new List<Color>();
            //        var localCardTypes = new List<CardType>();
            //        foreach (var card in cardsPage.CardList)
            //        {
            //            // Check if card is already in Db using CardId
            //            var cardIndex = context.CardIndices
            //                .First(c => c.CardId == card.Id);
            //            if (cardIndex != null)
            //                continue;

            //            // Create card index
            //            cardIndex = CardIndexFactory.GetCardIndex(card);
            //            // Add colors of card
            //            foreach (var cardColor in card.Colors)
            //            {
            //                // Check in Db
            //                var color = colors
            //                    .Where(c => c.Name == cardColor)
            //                    .FirstOrDefault();
            //                // Check local
            //                if (color == null)
            //                {
            //                    color = localColors
            //                        .Where(c => c.Name == cardColor)
            //                        .FirstOrDefault();
            //                }
            //                // Create new color
            //                if (color == null)
            //                {
            //                    color = new Color() { Name = cardColor };
            //                    colors.Add(color);
            //                    localColors.Add(color);
            //                }
            //                colorRelations.Add(new ColorRelation() { CardIndex = cardIndex, Color = color });
            //            }

            //            // Add types of card
            //            foreach (var rawCardType in card.Types)
            //            {
            //                // Check in Db
            //                var cardType = cardTypes
            //                    .Where(ct => ct.Name == rawCardType)
            //                    .FirstOrDefault();
            //                // Check local
            //                if (cardType == null)
            //                {
            //                    cardType = localCardTypes
            //                        .Where(ct => ct.Name == rawCardType)
            //                        .FirstOrDefault();
            //                }
            //                // Create new color
            //                if (cardType == null)
            //                {
            //                    cardType = new CardType() { Name = rawCardType };
            //                    cardTypes.Add(cardType);
            //                    localCardTypes.Add(cardType);
            //                }
            //                cardTypeRelations.Add(new CardTypeRelation() { CardIndex = cardIndex, CardType = cardType });
            //            }

            //            // Add card index
            //            cardIndices.Add(cardIndex);
            //        }

            //        // Commit page
            //        context.Commit();
            //        Thread.Sleep(500);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var msg = ex.Message;
            //}
        }
    }
}
