using Epic.Domain.Concrete;
using Epic.Domain.Entities;
using Epic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Models
{
    public static class StatsService
    {

public static void StartOP()
   {
    var repository = new EFPlayerRepository();
    List<PlayerStat> playerStats = new List<PlayerStat>();
    var ar = new int[] { 92};
    var arb = new int[] {98};
    for(int ii =0; ii<ar.Length;ii++)
    {
        int xx = arb[ii];
        int yy = ar[ii];
        var cl = repository.Clans.FirstOrDefault(x => x.ClanID == xx );
        IEnumerable<PlayerStat> pls = repository.PlayerStats.Where(x => x.clan.ClanID==xx);
    
    foreach(var i in pls)
    {
        i.clan = repository.Clans.FirstOrDefault(x => x.ClanID == yy);
        playerStats.Add(i);
    }
    }
    repository.SavePlayers(playerStats);
   }

    public static void Start()
        {
            var repository = new EFPlayerRepository();
            var time = DateTime.Today;
            var lastAge = repository.Ages.OrderByDescending(x=>x.StartTime).FirstOrDefault();
            if (lastAge != null)
            {
                var oldTime = DateTime.Today.AddDays(-7);
                var oldStats = repository.PlayerStats.Where(x => x.Time < oldTime).ToList();
                repository.DeleteStats(oldStats);
                var StartStats = repository.PlayerStats.Where(x => x.Time == lastAge.StartTime);
                var lastStats = repository.PlayerStats.Where(x => x.Time == time);
                Clans jclans = new Clans();
                List<PlayerStat> playerStats = new List<PlayerStat>();
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
                        var startStat = StartStats.FirstOrDefault(x => x.ID == jsPlayer.id);
                        var playerStat = lastStats.FirstOrDefault(x => x.ID == jsPlayer.id);
                        if (playerStat == null)
                        {
                            playerStat = new PlayerStat(jsPlayer, mclan, time);
                        }

                        if (startStat != null)
                        {
                            playerStat.frags = jsPlayer.frags;
                            playerStat.deaths = jsPlayer.deaths;
                            playerStat.curFrags = jsPlayer.frags - startStat.frags;
                            playerStat.curDeaths = jsPlayer.deaths - startStat.deaths;
                            playerStat.clan = mclan;
                        }
                        else
                        {
                            startStat = new PlayerStat(jsPlayer, mclan, lastAge.StartTime);
                            playerStats.Add(startStat);
                        }
                        playerStats.Add(playerStat);
                    }
                }
                repository.SaveClans(clans);
                repository.SavePlayers(playerStats);
                Log.Write();
            }
        }   
    }
}
