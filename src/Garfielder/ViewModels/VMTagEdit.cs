using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garfielder.ViewModels
{
    public class VMTagEdit:VMBase
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Slug { get; set; }
        public int CntTopic { get; set; }

        public bool IsNew
        {
            get
            {
                return Id.Equals(Guid.Empty);
            }
        }
    }
}