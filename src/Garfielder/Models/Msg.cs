using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.Models
{
    /// <summary>
    /// message sent by business object
    /// </summary>
    public class Msg
    {
        public bool Error { get; set; }
        public string Body { get; set; }
        public dynamic Context { get; set; }
    }
}