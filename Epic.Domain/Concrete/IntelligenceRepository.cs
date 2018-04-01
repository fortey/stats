using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Concrete
{
    public class IntelligenceRepository
    {
        private IntelligenceContext context = new IntelligenceContext();

        public IQueryable<PlayedDeck> PlayedDeckes
        {
            get { return context.PlayedDeckes.Include("DeckType"); }
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

        public void SavePlayedDeck(PlayedDeck playedDeck)
        {
            if (playedDeck.PlayedDeckId == 0)
            {
                context.PlayedDeckes.Add(playedDeck);
            }
            else
            {
                PlayedDeck dbEntry = context.PlayedDeckes.Find(playedDeck.PlayedDeckId);
                if (dbEntry != null)
                {
                    dbEntry.PlayerId = playedDeck.PlayerId;
                    dbEntry.Condition = playedDeck.Condition;
                    dbEntry.Date = playedDeck.Date;
                    dbEntry.DeckType = playedDeck.DeckType;
                    dbEntry.Description = playedDeck.Description;
                    dbEntry.User = playedDeck.User;
                }
            }       
            context.SaveChanges();
        }
    }
}
