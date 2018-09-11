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
using ProcessingClaim.DAL.Repositories.Interfaces;
using ProcessingClaim.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProcessingClaim.Web.Areas.Administration.Controllers
{
    public class RolesController : Controller
    {
        private IRoleRepository<Role, string> roleRepository = 
            new RoleRepository(new ProcessingClaimDbContext());

        // GET: Administration/Roles
        public ActionResult Index()
        {
            return View(roleRepository?.Roles());
        }


        // GET: Administration/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Role role)
        {
            if (ModelState.IsValid)
            {
                roleRepository.Create(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Administration/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleRepository.GetRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Administration/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] Role role)
        {
            if (ModelState.IsValid)
            {
                roleRepository.Edit(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Administration/Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleRepository.GetRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Administration/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            roleRepository.Delete(id);
            return RedirectToAction("Index");
        }        
    }
}
