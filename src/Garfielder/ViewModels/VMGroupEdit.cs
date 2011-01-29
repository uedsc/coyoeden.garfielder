using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Garfielder.ViewModels
{
    public class VMGroupEdit:VMBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Slug { get; set; }
        public string Description { get; set; }

        public int Level { get; set; }
        public bool Sys { get; set; }

        public Guid? ParentID { get; set; }
        public string ParentName { get; set; }

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