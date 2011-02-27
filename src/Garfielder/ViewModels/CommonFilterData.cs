using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonFilterData
    {
        /// <summary>
        /// action name
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// object id list
        /// </summary>
        public List<Guid> ObjIDList { get; set; }
    }
}