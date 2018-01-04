using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Models
{
    public class CurrentStat
    {
        public int ID { get; set; }
        public string nick { get; set; }
        public int level { get; set; }
        public int frags { get; set; }
        public int deaths { get; set; }
        public Clan clan { get; set; }
        public int curFrags { get; set; }
        public int curDeaths { get; set; }
    }
}
