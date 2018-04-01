using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Models
{
    public class ResultListModelView
    {
        public IEnumerable<ResultViewModel> ResultStats { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<SelectListItem> Clans { get; set; }
        public AgeResult AgeResult { get; set; }
    }

    public class ResultViewModel : ResultStat
    {
        public int position { get; set; }
        public int sodars { get; set; }
        public int games { get; set; }
        public string prize { get; set; }
        public int f_d { get; set; }
        public decimal coef { get; set; }
    }
}