using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Models;
using System.ComponentModel.DataAnnotations;

namespace Epic.Domain.Entities
{
    public class PlayerStat
    {
        [Key]
        public int PlayerStatID { get; set; }
        public int ID { get; set; }
        public string nick { get; set; }
        public int level { get; set; }
        public int frags { get; set; }
        public int deaths { get; set; }
        public Clan clan { get; set; }
        public DateTime Time { get; set; }
        public int curFrags { get; set; }
        public int curDeaths { get; set; }
        public PlayerStat(JsonPlayer jsPlayer, Clan clan, DateTime Time)
        {
            ID = jsPlayer.id;
            deaths = jsPlayer.deaths;
            frags = jsPlayer.frags;
            level=jsPlayer.level;
            nick = jsPlayer.nick;
            this.clan = clan;
            this.Time = Time;
        }
        public PlayerStat() { }
    }
}
