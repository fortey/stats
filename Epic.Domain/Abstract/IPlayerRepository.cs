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
        IQueryable<AgeResult> AgeResults { get; }
        IQueryable<ResultStat> ResultStats { get; }
        void SavePlayers(List<PlayerStat> players);
        void SaveClans(List<Clan> clans);
        void SaveAge(Age age);
        void SaveAgeResult(AgeResult ageResult);
        void SaveResults(List<ResultStat> players);
    }
}
