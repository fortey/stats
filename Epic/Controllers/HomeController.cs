using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using Epic.Domain.Models;
using Epic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace Epic.Controllers
{
    public class HomeController : Controller
    {
        
        private EFPlayerRepository repository;
        public int PageSize = 50;
        public HomeController()
        {
            this.repository = new EFPlayerRepository();
        }
        [HttpGet]
        public ActionResult Index( int clanId=91, int page=1)
        {
            if (clanId != 91) {page = 1;}
            var ps = repository.PlayerStats.OrderByDescending(x => x.Time).First();
            int count = repository.PlayerStats.Where(x => (x.clan.ClanID == clanId || clanId == 91) && x.Time == ps.Time).Count();
            var stats = repository.PlayerStats.Where(x => (x.clan.ClanID == clanId || clanId == 91) && x.Time == ps.Time).OrderByDescending(x => x.Time).ThenByDescending(x => x.curFrags).ThenByDescending(x => x.frags).Skip((page - 1) * PageSize).Take(PageSize);
            StatsListViewModel StatsModel = new StatsListViewModel
            {
                PlayerStats = op(stats, (page - 1) * PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                },
                Clans = new SelectList(repository.Clans, "ClanID", "title"),
                Date = DateTime.Today
            };
            ViewBag.IsEpic = User.IsInRole("Epic") || User.IsInRole("Admin");
            return View(StatsModel);
        }
        [HttpPost]
        public ActionResult Index(DateTime Date, int clanId = 17, int page = 1)
        {
            if (clanId != 17) { page = 1; }
            DateTime time = new DateTime();
            if (Date != DateTime.Today)
            {
                time = Date;
            }
            else
            {
                var ps = repository.PlayerStats.OrderByDescending(x => x.Time).First();
                time = ps.Time;
            }

            int count = repository.PlayerStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == time).Count();
            var stats = repository.PlayerStats.Where(x => (x.clan.ClanID == clanId || clanId == 17) && x.Time == time).OrderByDescending(x => x.Time).ThenByDescending(x => x.curFrags).ThenByDescending(x => x.frags).Skip((page - 1) * PageSize).Take(PageSize);
            
            StatsListViewModel StatsModel = new StatsListViewModel
            {
                PlayerStats = op(stats,(page-1)*PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                },
                Clans = new SelectList(repository.Clans, "ClanID", "title"),
                Date = Date
            };
            ViewBag.IsEpic = User.IsInRole("Epic") || User.IsInRole("Admin");
            if (clanId == 8)
                return View("Epic", StatsModel);
            else
                return View(StatsModel);
        }
        private IEnumerable<StatsViewModel> op(IEnumerable<PlayerStat> stats, int startPosition)
        {
           
            foreach (var s in stats)
            {
                startPosition += 1;
                var gms = s.curFrags+s.curDeaths;
                var pr = "";
                if(gms>=25){pr="Ультра";}
                else if(gms>=15){pr="1б + б.";}
                else if(gms>=10){pr="1б";}
                else if (gms >= 5) { pr = "Рарка"; }
                yield return new StatsViewModel { ID = s.ID, clan = s.clan, curDeaths = s.curDeaths, curFrags = s.curFrags, deaths = s.deaths, frags = s.frags, level = s.level, nick = s.nick,
                    position = startPosition, sodars = (int)(s.curFrags*2 + s.curDeaths*0.5), games = gms, prize = pr, f_d = s.curFrags-s.curDeaths, coef = s.curDeaths==0?0:(decimal)s.curFrags/s.curDeaths };
            }
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chart(int id)
        {            
            ViewBag.Id = id;
                return PartialView("Chart");
        }
        public ActionResult CreateChart(int id)
        {
            var lastAge = repository.Ages.OrderByDescending(x => x.StartTime).FirstOrDefault();
            var c = repository.PlayerStats.Where(x => x.ID == id && x.Time>lastAge.StartTime);
            var chart = new Chart(width: 850, height: 500)
            .AddTitle(c.First().nick)
                  .AddSeries(
                         name: "Фраги",
                         chartType: "Line",
                         xValue: c.Select(x=>x.Time).ToArray(),
                         yValues: c.Select(x => x.curFrags).ToArray())
                  .AddLegend()
                  .AddSeries(
                         name: "Cмерти",
                         chartType: "Line",
                         xValue: c.Select(x => x.Time).ToArray(),
                         yValues: c.Select(x => x.curDeaths).ToArray())
                         .AddLegend()
                  .Write();

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}