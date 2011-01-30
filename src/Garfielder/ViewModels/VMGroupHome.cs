using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMGroupHome:VMBase
    {
        public int TopicNum { get; set; }

        public VMGroupEdit GroupData { get; set; }

        public List<VMTopic> Topics { get; set; }
    }
}