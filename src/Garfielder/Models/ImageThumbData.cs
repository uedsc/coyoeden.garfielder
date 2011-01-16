using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.Models
{
    /// <summary>
    /// Image thumbnail poco
    /// </summary>
    public struct ImageThumbData
    {
        public string Flag { get; set; }
        public string Src { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}