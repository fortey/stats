using Epic.Domain.Abstract;
using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using Epic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Controllers
{
    public class AdminController : Controller
    {
        private IPlayerRepository repository;
        public AdminController()
        {
            this.repository = new EFPlayerRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult StartNewAge()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult StartNewAge(Age age)
        {
            age.StartTime = DateTime.Now ;
            Clans jclans= new Clans();
            List<PlayerStat> players = new List<PlayerStat>();
            List<Clan> clans = new List<Clan>();
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string json = wc.DownloadString("http://berserktcg.ru/api/export/clans.json");
            json = "{\"clans\":" + json + "}";
            jclans = JsonConvert.DeserializeObject<Clans>(json);
            foreach (var clan in jclans.clans)
            {
                json = wc.DownloadString("http://berserktcg.ru/api/export/clan/" + clan.id + ".json");
                JsonClan clann = JsonConvert.DeserializeObject<JsonClan>(json);
                var mclan = repository.Clans.FirstOrDefault(x => x.ID == clann.id);
                if (mclan == null)
                {
                    mclan = new Clan(clann);
                    clans.Add(mclan);
                } 
                foreach (var jsPlayer in clann.players)
                {                    
                    players.Add(new PlayerStat(jsPlayer,mclan,age.StartTime));
                }
            }
            repository.SaveAge(age);
            repository.SaveClans(clans);
            repository.SavePlayers(players);
            return View();
        }
	}
}