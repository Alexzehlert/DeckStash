using SearchMTG.domain.Cards.Models;
using SearchMTG.domain.Db;
using SearchMTG.domain.Factories;
using SearchMTG.domain.Models.Query;
using SearchMTG.domain.Models.Tracking;
using SearchMTG.domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SearchMTG.Controllers
{
    public class HomeController : Controller
    {
        public IMainContext context;

        public HomeController(IMainContext context)
        {
            this.context = context;
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("log-uuid")]
        public ActionResult PostLogUuid(string uuid)
        {
            try {
                var uuidLog = context.Select<UuidLog>().FirstOrDefault(x => x.Uuid == uuid);
                if (uuidLog == null) {
                    uuidLog = new UuidLog() { Uuid = uuid };
                    context.Select<UuidLog>().Add(uuidLog);
                }
                context.Select<UuidLogTimeStamp>().Add(new UuidLogTimeStamp() {
                    UuidLog = uuidLog,
                    TimeStamp = DateTime.UtcNow
                });
                context.Commit();
                return Json("success");
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("get-all-types")]
        public ActionResult GetAllTypes()
        {
            try {
                return Json(context.Select<CardType>().AsNoTracking().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [ActionName("get-all-colors")]
        public ActionResult GetAllColors()
        {
            try {
                return Json(context.Select<Color>().AsNoTracking().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [ActionName("get-all-sub-types")]
        public ActionResult GetAllSubTypes()
        {
            try {
                return Json(context.Select<CardSubType>().AsNoTracking().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [ActionName("get-all-sets")]
        public ActionResult GetAllSets()
        {
            try
            {
                return Json(context.Select<CardSet>().AsNoTracking().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [ActionName("get-all-rarity")]
        public ActionResult GetAllRarity()
        {
            try {
                return Json(context.Select<CardRarity>().AsNoTracking().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [ActionName("get-all-cmc")]
        public ActionResult GetAllCMC()
        {
            try {
                var cmcViewModel = GetCMCView(context.Select<CardInfo>().AsNoTracking());
                return Json(cmcViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private static CMCViewModel GetCMCView(IQueryable<CardInfo> queryable)
        {
            var cmcViewModel = new CMCViewModel() {
                Ranges = queryable
                    .Select(c => new { c.ConvertedManaCost })
                    .GroupBy(x => x.ConvertedManaCost, (key, group) => new CMCRangeViewModel { Cost = (int)key, Count = group.Count() })
                    .OrderBy(range => range.Cost)
                    .ToList()
            };
            cmcViewModel.Min = cmcViewModel.Ranges.First().Cost;
            cmcViewModel.Max = cmcViewModel.Ranges.Last().Cost;
            return cmcViewModel;
        }

        [HttpPost]
        [ActionName("get-cards")]
        public ActionResult GetCards(Query query)
        {
            var cardResponse = new CardResponseViewModel();
            try {
                IQueryable<CardInfo> queryable = context.Select<CardInfo>()
                    .AsNoTracking();

                // Filter by Color
                queryable = filterByColor(queryable, query.ColorIds);
                // Filter by Rarity
                queryable = filterByRarity(queryable, query.RarityIds);
                // Filter by Type
                queryable = filterByType(queryable, query.TypeIds);
                // Filter by Sub-Type
                queryable = filterBySubType(queryable, query.SubTypeIds);
                // Filter by Set
                queryable = filterBySubType(queryable, query.SetIds);
                // Filter by search input
                queryable = filterBySearchInput(queryable, query.InputSearch);

                // Get updated CMCRanges (before filtering by CMC)
                cardResponse.CMCs = GetCMCView(queryable);
                // Filter by Converted Mana Cost
                queryable = filterByConvertedManaCost(queryable, query.CmcRange[0], query.CmcRange[1]);
                
                // Sort and get page
                cardResponse.Cards = queryable
                    .OrderBy(c => c.Name)
                    .Skip(query.PageSize * query.PageNumber)
                    .Take(query.PageSize)
                    .ToList()
                    .Select(c => CardViewModelFactory.GetCardViewModel(c));
                
                return Json(cardResponse);
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }

        private static IQueryable<CardInfo> filterByConvertedManaCost(IQueryable<CardInfo> queryable, int min, int max)
        {
            return queryable.Where(card =>
                (int)card.ConvertedManaCost >= min && (int)card.ConvertedManaCost <= max
            );
        }

        private static IQueryable<CardInfo> filterByColor(IQueryable<CardInfo> queryable, List<int> colorIds)
        {
            if (colorIds == null)
                return queryable;
            return queryable.Where(card =>
                card.Colors.Count != 0
                &&
                colorIds.All(colorId => card.Colors.Any(color => color.Color.Id == colorId))
                
            );
        }

        private static IQueryable<CardInfo> filterByRarity(IQueryable<CardInfo> queryable, List<int> rarityIds)
        {
            if (rarityIds == null)
                return queryable;
            return queryable.Where(card =>
                rarityIds.Any(rarityId => rarityId == card.CardRarity.FirstOrDefault().CardRarity.Id)
            );
        }

        private static IQueryable<CardInfo> filterByType(IQueryable<CardInfo> queryable, List<int> typeIds)
        {
            if (typeIds == null)
                return queryable;
            return queryable.Where(card =>
                card.Types.Any(tr => typeIds.Any(typeId => typeId == tr.Type.Id))
            );
        }

        private static IQueryable<CardInfo> filterBySubType(IQueryable<CardInfo> queryable, List<int> subtypeIds)
        {
            if (subtypeIds == null)
                return queryable;
            return queryable.Where(card =>
                card.SubTypes.Any(str => subtypeIds.Any(subtypeId => subtypeId == str.SubType.Id))
            );
        }

        private static IQueryable<CardInfo> filterBySet(IQueryable<CardInfo> queryable, List<int> setIds)
        {
            if (setIds == null)
                return queryable;
            return queryable.Where(card =>
                setIds.Any(setId => setId == card.CardSet.FirstOrDefault().CardSet.Id)
            );
        }

        private static IQueryable<CardInfo> filterBySearchInput(IQueryable<CardInfo> queryable, string searchInput)
        {
            if (searchInput == null)
                return queryable;
            searchInput = searchInput.ToLower();
            return queryable.Where(card =>
                card.Name.ToLower().Contains(searchInput)
            );
        }
    }
}