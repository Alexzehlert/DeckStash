namespace SearchMTG.integration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 60),
                        ManaCost = c.String(maxLength: 30),
                        ConvertedManaCost = c.Single(nullable: false),
                        TypeName = c.String(maxLength: 60),
                        Text = c.String(maxLength: 800),
                        Flavor = c.String(maxLength: 700),
                        Power = c.String(maxLength: 10),
                        Toughness = c.String(maxLength: 10),
                        Price = c.Double(nullable: false),
                        Artist = c.String(maxLength: 50),
                        NormalImage = c.String(maxLength: 100),
                        CroppedImage = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardRarityRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.Int(nullable: false),
                        CardRarity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.CardRarities", t => t.CardRarity_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.CardRarity_Id);
            
            CreateTable(
                "dbo.CardRarities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardSetRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.Int(nullable: false),
                        CardSet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.CardSets", t => t.CardSet_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.CardSet_Id);
            
            CreateTable(
                "dbo.CardSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Abbv = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardColorRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.Int(nullable: false),
                        Color_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.Color_Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Abbv = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardSubTypeRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.Int(nullable: false),
                        SubType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.CardSubTypes", t => t.SubType_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.SubType_Id);
            
            CreateTable(
                "dbo.CardSubTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardTypeRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.Int(nullable: false),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.CardTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.CardTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UuidLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uuid = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UuidLogTimeStamps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        UuidLog_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UuidLogs", t => t.UuidLog_Id, cascadeDelete: true)
                .Index(t => t.UuidLog_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UuidLogTimeStamps", "UuidLog_Id", "dbo.UuidLogs");
            DropForeignKey("dbo.CardTypeRelations", "Type_Id", "dbo.CardTypes");
            DropForeignKey("dbo.CardTypeRelations", "Card_Id", "dbo.CardInfoes");
            DropForeignKey("dbo.CardSubTypeRelations", "SubType_Id", "dbo.CardSubTypes");
            DropForeignKey("dbo.CardSubTypeRelations", "Card_Id", "dbo.CardInfoes");
            DropForeignKey("dbo.CardColorRelations", "Color_Id", "dbo.Colors");
            DropForeignKey("dbo.CardColorRelations", "Card_Id", "dbo.CardInfoes");
            DropForeignKey("dbo.CardSetRelations", "CardSet_Id", "dbo.CardSets");
            DropForeignKey("dbo.CardSetRelations", "Card_Id", "dbo.CardInfoes");
            DropForeignKey("dbo.CardRarityRelations", "CardRarity_Id", "dbo.CardRarities");
            DropForeignKey("dbo.CardRarityRelations", "Card_Id", "dbo.CardInfoes");
            DropIndex("dbo.UuidLogTimeStamps", new[] { "UuidLog_Id" });
            DropIndex("dbo.CardTypeRelations", new[] { "Type_Id" });
            DropIndex("dbo.CardTypeRelations", new[] { "Card_Id" });
            DropIndex("dbo.CardSubTypeRelations", new[] { "SubType_Id" });
            DropIndex("dbo.CardSubTypeRelations", new[] { "Card_Id" });
            DropIndex("dbo.CardColorRelations", new[] { "Color_Id" });
            DropIndex("dbo.CardColorRelations", new[] { "Card_Id" });
            DropIndex("dbo.CardSetRelations", new[] { "CardSet_Id" });
            DropIndex("dbo.CardSetRelations", new[] { "Card_Id" });
            DropIndex("dbo.CardRarityRelations", new[] { "CardRarity_Id" });
            DropIndex("dbo.CardRarityRelations", new[] { "Card_Id" });
            DropTable("dbo.UuidLogTimeStamps");
            DropTable("dbo.UuidLogs");
            DropTable("dbo.CardTypes");
            DropTable("dbo.CardTypeRelations");
            DropTable("dbo.CardSubTypes");
            DropTable("dbo.CardSubTypeRelations");
            DropTable("dbo.Colors");
            DropTable("dbo.CardColorRelations");
            DropTable("dbo.CardSets");
            DropTable("dbo.CardSetRelations");
            DropTable("dbo.CardRarities");
            DropTable("dbo.CardRarityRelations");
            DropTable("dbo.CardInfoes");
        }
    }
}
