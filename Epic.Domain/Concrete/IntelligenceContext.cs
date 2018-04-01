using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Concrete
{
    class IntelligenceContext : DbContext
    {
        public DbSet<DeckType> DeckTypes { get; set; }
        public DbSet<PlayedDeck> PlayedDeckes { get; set; }
    }
}
