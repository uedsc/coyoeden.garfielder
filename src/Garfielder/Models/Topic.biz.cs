﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.ViewModels;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Models
{
    public partial class Topic
    {
        /// <summary>
        /// validate a slug
        /// </summary>
        /// <param name="slug">slug</param>
        /// <param name="db">GarfielderEntities instance</param>
        /// <returns></returns>
        public static Msg ValidateSlug(string slug,GarfielderEntities db=null)
        {
            var r = new Msg();
            if (string.IsNullOrWhiteSpace(slug))
            {
                r.Error = true;
                r.Body = "Slug can't be empty!";
                r.Context["Slug"] = Utils.RandomStr(8);
                return r;
            }
            var isNewConnection = db == null;
            db = db ?? new GarfielderEntities();
            r.Context["Slug"] = slug;
            try
            {
                var topic = db.Topics.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
                if (topic == null) return r;
                r.Error = true;
                r.Body = string.Format("Slug [{0}] has been used by another topic!", slug);
                r.Context["Slug"] = string.Format("{0}{1}", slug, Utils.RandomStr(4));
            }
            catch (Exception ex)
            {

                r.Error = true;
                r.Body = string.Format("Error:{0}", ex.Message);
            }finally
            {
                if (isNewConnection)
                    db.Dispose();
            }
            return r;

        }
        
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

                vm.Groups = dbm.Groups.ToList();

                vm.Tags = dbm.Tags.ToList();

                return vm; 

            };
        }
        public static Msg DeleteByID(params Guid[] id)
        {
            var r = new Msg();
            if(id==null||id.Length==0)
            {
                r.Error = true;
                r.Body = "No selected topics to be deleted!";
                return r;
            }
            using (var db=new GarfielderEntities())
            {
                try
                {
                    var items = db.Topics.Where(x => id.Contains(x.Id)).ToList();
                    items.ForEach(obj=> {
                        obj.Tags.Clear();
                        obj.Groups.Clear();
                        db.Topics.DeleteObject(obj);
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    r.Error = true;
                    r.Body = ex.Message;
                }
            }//using
            return r;
        }
    }
}