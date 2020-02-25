using SearchMTG.domain.Cards.Models;
using SearchMTG.domain.Models.Cards.Relations;
using SearchMTG.domain.Models.Tracking;
using System.Data.Entity;

namespace SearchMTG.domain.Db
{
    public interface IMainContext
    {
        DbSet<T> Select<T>() where T : class;
        void Commit();
        void Dispose();
    } 

    public class MainContext : DbContext, IMainContext
    {
        public DbSet<UuidLog> UuidLogs { get; set; }
        public DbSet<UuidLogTimeStamp> UuidLogTimeStamps { get; set; }

        public DbSet<CardInfo> CardIndices { get; set; }

        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<CardTypeRelation> CardTypeRelations { get; set; }

        public DbSet<CardSubType> CardSubTypes { get; set; }
        public DbSet<CardSubTypeRelation> CardSubTypeRelations { get; set; }

        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<CardSetRelation> CardSetRelations { get; set; }

        public DbSet<CardRarity> CardRarities { get; set; }
        public DbSet<CardRarityRelation> CardRarityRelations { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<CardColorRelation> ColorRelations { get; set; }

        public MainContext() : base("SearchMTG.Db")
        {
        }

        public DbSet<T> Select<T>() where T : class
        {
            return Set<T>();
        }
        
        public void Commit()
        {
            SaveChanges();
        }
    }
}
