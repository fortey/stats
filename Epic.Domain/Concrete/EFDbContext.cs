using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Epic.Domain.Entities;

namespace Epic.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<PlayerStat> PlayerStats { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Age> Ages { get; set; }
        public DbSet<AgeResult> AgeResults { get; set; }
        public DbSet<ResultStat> ResultStats { get; set; }
    }
}
