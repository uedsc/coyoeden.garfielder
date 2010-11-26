using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMCampTopicEdit:VMBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ContentX { get; set; }
        public double Grade { get; set; }
        public string UserName { get; set; }
        public DateTime CreateAt { get; set; }
        public int CntComment { get; set; }
        public string Groups { get; set; }
        public string Tags { get; set; }
    }
}