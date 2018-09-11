using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcessingClaim.DAL.Models;
using ProcessingClaim.Web.Models;
using ProcessingClaim.DAL.Repositories;

namespace ProcessingClaim.Web.Areas.Administration.Controllers
{
    public class PeopleController : Controller
    {
        private UserRepository userRepository = new UserRepository(new ProcessingClaimDbContext());
        private RoleRepository roleRepository = new RoleRepository(new ProcessingClaimDbContext());

        // GET: Administration/People
        public ActionResult Index()
        {
            return View(userRepository?.Users());
        }

        // GET: Administration/People/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = userRepository?.GetUser(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Administration/People/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(roleRepository.Roles(), "Id", "Title");
            return View();
        }

        // POST: Administration/People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Password,RoleId")] Person person)
        {
            if (ModelState.IsValid)
            {
                userRepository?.Create(person);
                return RedirectToAction("Index");
            }
            
            return View(person);
        }

        // GET: Administration/People/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = userRepository?.GetUser(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(roleRepository.Roles(), "Id", "Title");
            return View(person);
        }

        // POST: Administration/People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RoleId")] Person person)
        {
            if (ModelState.IsValid)
            {
                userRepository?.Edit(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Administration/People/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = userRepository?.GetUser(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Administration/People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            userRepository?.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
