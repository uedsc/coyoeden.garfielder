using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.Models
{
    public struct ImageFlags
    {
        /// <summary>
        /// 800x600
        /// </summary>
        public static readonly string L800X600 = "800x600";
        /// <summary>
        /// 500x500
        /// </summary>
        public static readonly string L500X500 = "500x500";
        /// <summary>
        /// 300x300
        /// </summary>
        public static readonly string M300X300 = "300x300";
        /// <summary>
        /// 160x160
        /// </summary>
        public static readonly string M160X160 = "160x160";
        /// <summary>
        /// 160x100
        /// </summary>
        public static readonly string S160X100 = "160x100";
        /// <summary>
        /// 64x64
        /// </summary>
        public static readonly string S64X64 = "64x64";
        /// <summary>
        /// RAW image
        /// </summary>
        public static readonly string RAW = "";
    }
}