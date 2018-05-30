using Epic.Domain.Abstract;
using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Concrete
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<PlayerStat> PlayerStats
        {
            get { return context.PlayerStats.Include("Clan"); }
        }
        public IQueryable<Clan> Clans
        {
            get { return context.Clans; }
        }
        public IQueryable<Age> Ages
        {
            get { return context.Ages; }
        }

        public IQueryable<ResultStat> ResultStats
        {
            get { return context.ResultStats.Include("Clan"); }
        }
        public IQueryable<AgeResult> AgeResults
        {
            get { return context.AgeResults; }
        }

        public void SavePlayers(List<PlayerStat> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerStatID == 0)
                {
                    context.PlayerStats.Add(player);
                }
                else
                {
                    PlayerStat dbEntry = context.PlayerStats.Find(player.PlayerStatID);
                    if (dbEntry != null)
                    {
                        dbEntry.nick = player.nick;
                        dbEntry.level = player.level;
                        dbEntry.frags = player.frags;
                        dbEntry.deaths = player.deaths;
                        dbEntry.clan = player.clan;
                        dbEntry.Time = player.Time;
                    }
                }
            }
            context.SaveChanges();
        }

        public void SaveResults(List<ResultStat> players)
        {
            foreach (var player in players)
            {
                if (player.ResultStatID == 0)
                {
                    context.ResultStats.Add(player);
                }
                else
                {
                    ResultStat dbEntry = context.ResultStats.Find(player.ResultStatID);
                    if (dbEntry != null)
                    {
                        dbEntry.nick = player.nick;
                        dbEntry.level = player.level;
                        dbEntry.frags = player.frags;
                        dbEntry.deaths = player.deaths;
                        dbEntry.clan = player.clan;
                        dbEntry.Time = player.Time;
                    }
                }
            }
            context.SaveChanges();
        }

        public void SaveClans(List<Clan> clans)
        {
            foreach (var clan in clans)
            {
                Clan dbEntry = context.Clans.FirstOrDefault(x=>x.ID==clan.ID);
                if (dbEntry == null)
                {
                    context.Clans.Add(clan);
                }
            }
            context.SaveChanges();
        }
        public void SaveAge(Age age)
        {        
                Age dbEntry = context.Ages.FirstOrDefault(x=>x.Number==age.Number);
                if (dbEntry != null)
                {
                    dbEntry.StartTime = age.StartTime;
                }
                else
                    context.Ages.Add(age);
            
            context.SaveChanges();
        }

        public void SaveAgeResult(AgeResult age)
        {
            AgeResult dbEntry = context.AgeResults.FirstOrDefault(x => x.AgeResultID == age.AgeResultID);
            if (dbEntry != null)
            {
                dbEntry.Time = age.Time;
                dbEntry.Name = age.Name;
                dbEntry.Price = age.Price;
            }
            else
                context.AgeResults.Add(age);

            context.SaveChanges();
        }

        public void DeleteStats(List<PlayerStat> stats)
        {
            foreach (var stat in stats)
            {
                PlayerStat dbEntry = context.PlayerStats.Find(stat.PlayerStatID);
                if (dbEntry != null)
                {
                    context.PlayerStats.Remove(dbEntry);
                }
            }
            context.SaveChanges();
        }

        public IQueryable<DeckType> DeckTypes
        {
            get { return context.DeckTypes; }
        }

        public void SaveDeckType(DeckType deckType)
        {
            if (deckType.DeckTypeId == 0)
            {
                context.DeckTypes.Add(deckType);
            }
            else
            {
                DeckType dbEntry = context.DeckTypes.Find(deckType.DeckTypeId);
                if (dbEntry != null)
                {
                    dbEntry.Name = deckType.Name;
                    dbEntry.Description = deckType.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
