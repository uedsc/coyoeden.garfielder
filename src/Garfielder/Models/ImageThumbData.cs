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
        /// <summary>
        /// size flag
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// image src
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// height
        /// </summary>
        public int Height { get; set; }
    }
}