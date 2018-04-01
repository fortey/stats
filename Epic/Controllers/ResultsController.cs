using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using Epic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Controllers
{
    public class ResultsController : Controller
    {
        private EFPlayerRepository repository;
        public int PageSize = 50;
        public ResultsController()
        {
            this.repository = new EFPlayerRepository();
        }

        public ActionResult Index()
        {
            return View(repository.AgeResults.OrderByDescending(x=>x.Time));
        }

        //[HttpGet]
        public ActionResult List(int ageresult, int clanId = 17, int page = 1)
        {
            var result = repository.AgeResults.FirstOrDefault(x => x.AgeResultID == ageresult);
            if (result == null)
            {
                return View();
            }
            if (clanId != 17) { page = 1; }
            int count = repository.ResultStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == result.Time).Count();
            var stats = repository.ResultStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == result.Time).OrderByDescending(x => x.curFrags).ThenBy(x=>x.curDeaths).ThenByDescending(x => x.frags).Skip((page - 1) * PageSize).Take(PageSize);
            ResultListModelView StatsModel = new ResultListModelView
            {
                AgeResult = result,
                ResultStats = op(stats, (page - 1) * PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                },
                Clans = new SelectList(repository.Clans, "ClanID", "title")
            };
            return View(StatsModel);
        }
        //[HttpPost]
        //public ActionResult List(int ageresult, int clanId = 17, int page = 1)
        //{
        //    if (clanId != 17) { page = 1; }
        //    var result = repository.AgeResults.FirstOrDefault(x => x.AgeResultID == ageresult);
        //    if (result == null)
        //    {
        //        return View();
        //    }
        //    int count = repository.ResultStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == result.Time).Count();
        //    var stats = repository.ResultStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == result.Time).OrderByDescending(x => x.curFrags).ThenBy(x => x.curDeaths).ThenByDescending(x => x.frags).Skip((page - 1) * PageSize).Take(PageSize);

        //    ResultListModelView StatsModel = new ResultListModelView
        //    {
        //        AgeResult = result,
        //        ResultStats = op(stats, (page - 1) * PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = count
        //        },
        //        Clans = new SelectList(repository.Clans, "ClanID", "title"),
        //    };         
        //    return View(StatsModel);
        //}
        private IEnumerable<ResultViewModel> op(IEnumerable<ResultStat> stats, int startPosition)
        {

            foreach (var s in stats)
            {
                startPosition += 1;
                var gms = s.curFrags + s.curDeaths;
                var pr = "";
                if (gms >= 25) { pr = "Ультра"; }
                else if (gms >= 15) { pr = "1б + б."; }
                else if (gms >= 10) { pr = "1б"; }
                else if (gms >= 5) { pr = "Рарка"; }
                yield return new ResultViewModel
                {
                    ID = s.ID,
                    clan = s.clan,
                    curDeaths = s.curDeaths,
                    curFrags = s.curFrags,
                    deaths = s.deaths,
                    frags = s.frags,
                    level = s.level,
                    nick = s.nick,
                    position = startPosition,
                    sodars = (int)(s.curFrags * 2 + s.curDeaths * 0.5),
                    games = gms,
                    prize = pr,
                    f_d = s.curFrags - s.curDeaths,
                    coef = s.curDeaths == 0 ? 0 : (decimal)s.curFrags / s.curDeaths
                };
            }
        }
    }    
}