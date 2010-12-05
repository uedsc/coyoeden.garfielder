using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Garfielder.ViewModels
{
    public class VMTagList:VMBase
    {
        public List<VMTagEdit> TagList { get; set; }

        //edit view data
        [Required]
        [StringLength(50)]
        [Remote("TagName", "FBI", HttpMethod = "Post")]
        public string Name { get; set; }
        [Remote("TagSlug", "FBI", HttpMethod = "Post")]
        public string Slug { get; set; }
    }
}