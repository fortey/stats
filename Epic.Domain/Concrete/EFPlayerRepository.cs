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
    }
}
