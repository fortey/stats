using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epic.Domain.Entities
{
    public class PlayedDeck
    {
        public int PlayedDeckId { get; set; }
        public DeckType DeckType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Condition { get; set; }
        public int Player { get; set; }
        [MaxLength(30)]
        public string User { get; set; }
    }
}