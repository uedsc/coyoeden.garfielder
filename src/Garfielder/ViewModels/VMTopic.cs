using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garfielder.ViewModels
{
    public class VMTopic:VMBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string XContent { get; set; }
        public string Logo { get; set; }
        public DateTime DateCreated { get; set; }

    }
}