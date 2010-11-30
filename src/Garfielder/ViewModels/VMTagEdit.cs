using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMTagEdit:VMBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}