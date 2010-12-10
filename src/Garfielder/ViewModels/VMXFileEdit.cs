using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMXFileEdit:VMBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        public bool NoFlash { get; set; }
        /// <summary>
        /// Name without extension
        /// </summary>
        public string Name1 { get; set; }
    }
}