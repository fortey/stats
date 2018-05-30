using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Domain.Entities
{
    public class AgeResult
    {
        public int AgeResultID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Time { get; set; }
    }

    public class ResultStat
    {
        [Key]
        public int ResultStatID { get; set; }
        public int ID { get; set; }
        public string nick { get; set; }
        public int level { get; set; }
        public int frags { get; set; }
        public int deaths { get; set; }
        public Clan clan { get; set; }
        public DateTime Time { get; set; }
        public int curFrags { get; set; }
        public int curDeaths { get; set; }
    }
}
