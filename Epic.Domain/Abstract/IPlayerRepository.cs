using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Abstract
{
    public interface IPlayerRepository
    {
        IQueryable<PlayerStat> PlayerStats { get; }
        IQueryable<Clan> Clans { get; }
        IQueryable<Age> Ages { get; }
        void SavePlayers(List<PlayerStat> players);
        void SaveClans(List<Clan> clans);
        void SaveAge(Age age);
    }
}
