using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMUserList:VMBase
    {
        public List<VMUserEdit> UserList { get; set; }
    }
}