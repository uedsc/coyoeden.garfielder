using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMCampHome:VMBase
    {
        /// <summary>
        /// today's topic count 
        /// </summary>
        public int CntTopicToday { get; set; }
        /// <summary>
        /// today's comment count
        /// </summary>
        public int CntCommentToday { get; set; }
        /// <summary>
        /// total topic number
        /// </summary>
        public int CntTopic { get; set; }
        /// <summary>
        /// total comment number
        /// </summary>
        public int CntComment { get; set; }
    }
}