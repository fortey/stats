using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epic.Models
{
    public class JsonPlayer
    {

        public int id { get; set; }
        public string nick { get; set; }
        public int level { get; set; }
        public int frags { get; set; }
        public int deaths { get; set; }
    }

    public class Clans
    {
        public JsonClan[] clans { get; set; }
    }
    public class JsonClan
    {
        public int id { get; set; }
        public object abbr { get; set; }
        public string title { get; set; }
        public int points { get; set; }
        public string created { get; set; }
        public string site { get; set; }
        public List<int> cities { get; set; }
        public List<JsonPlayer> players { get; set; }
    }
}