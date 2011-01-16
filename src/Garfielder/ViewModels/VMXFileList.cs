using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Models;

namespace Garfielder.ViewModels
{
    public class VMXFileList:VMBase
    {
        public List<VMXFileEdit> FileList { get; set; }
        public Topic RefTopic { get; set; }
    }
}