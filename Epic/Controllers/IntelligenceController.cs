using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Controllers
{
    public class IntelligenceController : Controller
    {
        IntelligenceRepository intelRepository = new IntelligenceRepository();
        // GET: Intelligence
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewRecord(int playerId)
        {
            ViewBag.playerId = playerId;
            return View();
        }

        [HttpPost]
        public ActionResult NewRecord(PlayedDeck playedDeck)
        {
            playedDeck.User = User.Identity.Name;
            intelRepository.SavePlayedDeck(playedDeck);
            return View("PlayerInfo", intelRepository.PlayedDeckes.Where(x => x.PlayerId == playedDeck.PlayerId));
        }

        [HttpGet]
        public ActionResult NewDeckType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewDeckType(DeckType deckType)
        {
            intelRepository.SaveDeckType(deckType);
            return View("DeckTypesList");
        }

        public ActionResult DeckTypesList()
        {
            return View(intelRepository.DeckTypes);
        }

        public ActionResult PlayerInfo(int playerId)
        {
            ViewBag.playerId = playerId;
            return View(intelRepository.PlayedDeckes.Where(x => x.PlayerId == playerId));
        }
    }
}