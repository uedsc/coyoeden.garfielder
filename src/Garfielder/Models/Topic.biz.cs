using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.ViewModels;

namespace Garfielder.Models
{
    public partial class Topic
    {
        public static VMCampTopicShow  GetTopic(Guid id) {
            using (var db = new GarfielderEntities())
            {
                
                var dbm= db.Topics.Single(x => x.Id == id);
                if (dbm == null) return null;
                var vm = new VMCampTopicShow();
                vm.Id = dbm.Id;
                vm.Title = dbm.Title;
                vm.Slug = dbm.Slug;
                vm.Url = dbm.Url;
                vm.Grade = dbm.Grade;
                vm.Description = dbm.Description;
                vm.ContentX = dbm.ContentX;

                vm.GroupsAll =Group.ListAll();
                vm.Groups = dbm.Groups.ToList();

                vm.TagsAll = Tag.ListAll();
                vm.Tags = dbm.Tags.ToList();

                return vm; 

            };
        }
    }
}