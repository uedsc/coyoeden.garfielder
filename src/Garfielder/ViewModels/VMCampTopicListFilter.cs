using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMCampTopicListFilter:VMCampCommonFilter
    {
        public string Date { get; set; }
        
        public string GroupID { get; set; }

        public string term { get; set; }
        public bool published { get; set; }
    }
}