using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMGroupList:VMBase
    {
        public List<VMGroupEdit> GroupList { get; set; }
    }
}