using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Controllers
{
    [Authorize(Roles = "Epic")]
    public class IntelligenceController : Controller
    {
        IntelligenceRepository intelRepository = new IntelligenceRepository();
        EFPlayerRepository statsRepository = new EFPlayerRepository();
        // GET: Intelligence
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult NewRecord(int playerId)
        {
            ViewBag.playerId = playerId;       
            ViewBag.deckTypeList = new SelectList(statsRepository.DeckTypes, "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult NewRecord(PlayedDeck playedDeck)
        {
            playedDeck.Date = DateTime.Now;
            playedDeck.User = User.Identity.Name;
            var x = intelRepository.SavePlayedDeck(playedDeck, playedDeck.PlayerId);
            return RedirectToAction("PlayerInfo", new { playerId = playedDeck.PlayerId });
        }

        [HttpGet]
        public ActionResult NewDeckType(int playerId = 0)
        {
            ViewBag.playerId = playerId;
            return View();
        }

        [HttpPost]
        public ActionResult NewDeckType(DeckType deckType, int playerId = 0)
        {
            if (statsRepository.DeckTypes.Where(x => x.Name == deckType.Name).Count() > 0)
                return View();
            statsRepository.SaveDeckType(deckType);
            if (playerId == 0)
            {                
                return RedirectToAction("DeckTypesList");
            }
            else
            {        
                return RedirectToAction("NewRecord",new { playerId });
            }
        }

        public ActionResult DeckTypesList()
        {
            return View(statsRepository.DeckTypes);
        }

        public ActionResult PlayerInfo(int playerId)
        {                   
            var player = statsRepository.PlayerStats.FirstOrDefault(x => x.ID == playerId);
            if (player != null)
                ViewBag.Title = player.nick + " (" + playerId + ")";
            ViewBag.playerId = playerId;
            var records = intelRepository.PlayedDeckes(playerId).Result;
            if(records.Count>0)
                return View(records.Select(x => x.Object).OrderByDescending(x => x.Date));
            return View();
        }
    }
}