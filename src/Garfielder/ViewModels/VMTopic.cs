using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string XContent { get; set; }
        public string Icon { get; set; }
        public DateTime DateCreated { get; set; }
    }
}