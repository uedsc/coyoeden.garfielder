using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Garfielder.ViewModels
{
    public class VMGroupList:VMBase
    {
        //list data
        public List<VMGroupEdit> GroupList { get; set; }

        //edit field data-fields should be same as VMGroupEdit
        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        [Remote("GroupName", "FBI", HttpMethod = "Post",AdditionalFields="Id")]
        public string Name { get; set; }
        [StringLength(50)]
        [Remote("GroupSlug", "FBI", HttpMethod = "Post",AdditionalFields="Id")]
        public string Slug { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public Guid? ParentID { get; set; }
        public Guid Id { get; set; }

    }
}