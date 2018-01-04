using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Epic.Domain.Entities
{
    public class DeckType
    {
        public int DeckTypeId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}