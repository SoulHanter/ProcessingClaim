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
using PagedList;
using PagedList.Mvc;

namespace ProcessingClaim.Web.Controllers
{
    public class ClaimsController : Controller
    {
        private ProcessingClaimDbContext db = new ProcessingClaimDbContext();

        // GET: Claims
        public ActionResult Index(int page = 1)
        {
            int pageSize = 5;
            int pageNumber = page;

            var claims = db.Claims.Include(c => c.Category);
            return View(claims.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Claims/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // GET: Claims/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title");
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,FIO,PhoneNumber,Text,CreationOn,CategoryId")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Id = Guid.NewGuid();
                claim.AuthorId = User.Identity.Name;
                db.Claims.Add(claim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", claim.CategoryId);
            return View(claim);
        }

        // GET: Claims/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", claim.CategoryId);
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Status")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                Claim changeClaim = db.Claims.Find(claim.Id);
                if (changeClaim == null)
                {
                    return HttpNotFound();
                }

                changeClaim.Status = claim.Status;
                changeClaim.Text = claim.Text;

                db.Entry(changeClaim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", claim.CategoryId);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Claim claim = db.Claims.Find(id);
            db.Claims.Remove(claim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
