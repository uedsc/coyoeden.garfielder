using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garfielder.Controllers
{
    public class XFileController : Controller
    {
        //
        // GET: /XFile/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /XFile/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /XFile/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /XFile/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /XFile/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /XFile/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /XFile/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /XFile/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
