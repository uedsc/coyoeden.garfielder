using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public partial class VMUploadMedia:VMXFileEdit
    {
        /// <summary>
        /// topic id
        /// </summary>
        public Guid RefId { get; set; }
        /// <summary>
        /// upload source
        /// </summary>
        public string Src { get; set; }

        public List<VMXFileEdit> FileList { get; set; }
        /// <summary>
        /// list view mode
        /// </summary>
        public string ViewMode { get; set; }
		/// <summary>
		/// topic's attachments
		/// </summary>
		public List<Guid> RefAttachments { get; set; }
    }
}