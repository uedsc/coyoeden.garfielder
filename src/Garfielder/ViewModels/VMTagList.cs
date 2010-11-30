using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMTagList:VMBase
    {
        public List<VMTagEdit> TagList { get; set; }
    }
}