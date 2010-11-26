using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMXFileList:VMBase
    {
        public List<VMXFileEdit> FileList { get; set; }
    }
}