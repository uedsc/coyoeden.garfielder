using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Models
{
    /// <summary>
    /// biz logic for Group.TODO:via repository pattern
    /// </summary>
    public partial class Group
    {
        public static bool ValidateName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities()) {
                valid = db.Groups.Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }   
        }
        public static bool ValidateSlug(string slug) {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities()) {
                valid = db.Groups.Count(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }
        }
    }
    
}