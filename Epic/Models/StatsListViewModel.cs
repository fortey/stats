using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Models
{
    public class StatsListViewModel
    {
        public IEnumerable<StatsViewModel> PlayerStats { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<SelectListItem> Clans { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }

    public class StatsViewModel:PlayerStat
    {    
        public int position { get; set; }
        public int sodars { get; set; }
        public int games { get; set; }
        public string prize { get; set; }
        public int f_d { get; set; }
        public decimal coef { get; set; }

    }
}