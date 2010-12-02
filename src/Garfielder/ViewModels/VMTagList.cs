using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garfielder.ViewModels
{
    public class VMTagList:VMBase
    {
        public List<VMTagEdit> TagList { get; set; }

        //edit view data
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}