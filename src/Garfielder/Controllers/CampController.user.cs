using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;

namespace Garfielder.Controllers
{
    public partial class CampController
    {
        public ActionResult ListUser()
        {
            var vm = CreateViewData<VMUserList>();
            using (var db = new GarfielderEntities()) {
                var users = db.Users.ToList();
                vm.UserList=new List<VMUserEdit>();
                users.ForEach(x => {
                    vm.UserList.Add(new VMUserEdit { 
                        Name=x.Name,
                        Nickname=x.Nickname,
                        Email=x.Email,
                        Password=x.Password,
                        Id=x.Id
                    });
                });
            }
            return View(vm);
        }
        [HttpGet]
        public ActionResult EditUser(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NewUser();
            };
            var vm = CreateViewData<VMUserEdit>();
            using (var db = new GarfielderEntities())
            {
                var dbModel = db.Users.Single(x => x.Id == id.Value);
                vm.Id = dbModel.Id;
                vm.Name = dbModel.Name;
                vm.Nickname = dbModel.Nickname;
                vm.Email = dbModel.Email;
                vm.Password = dbModel.Password;
            };
            return View(vm);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditUser(VMUserEdit obj)
        {
            if (this.ModelState.IsValid)
            {
                var dbm = new User();
                dbm.Id = Guid.NewGuid();
                dbm.Name = obj.Name;
                dbm.Nickname = obj.Nickname;
                dbm.Email = obj.Email;
                dbm.Password = obj.Password;
                dbm.CreatedAt = DateTime.Now;
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    db.CommandTimeout = 0;
                    db.AddToUsers(dbm);
                    db.SaveChanges();
                };
            };
            return View(obj);
        }
        private ActionResult NewUser()
        {
            var vm = CreateViewData<VMUserEdit>();
            vm.Id = Guid.NewGuid();
            return View(vm);
        }
    }
}