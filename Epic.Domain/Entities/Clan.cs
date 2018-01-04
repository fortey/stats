using Epic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Epic.Domain.Entities
{
    public class Clan
    {
        [Key] 
        public int ClanID { get; set; }
        public int ID { get; set; }
        public object abbr { get; set; }
        public string title { get; set; }

        public Clan(JsonClan jsClan)
        {
            ID = jsClan.id;
            abbr = jsClan.abbr;
            title = jsClan.title;
        }
        public Clan()
        {
        }
    }
}
