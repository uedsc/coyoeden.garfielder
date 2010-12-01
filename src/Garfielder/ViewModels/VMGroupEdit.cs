using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garfielder.ViewModels
{
    public class VMGroupEdit:VMBase
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage="Name is required")]
        [StringLength(50)]
        public string Name { get; set; }
        
        public string Slug { get; set; }
        public string Description { get; set; }

        public int Level { get; set; }

        public Guid? ParentID { get; set; }
        public string ParentName { get; set; }
    }
}