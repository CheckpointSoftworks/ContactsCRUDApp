using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactsCRUDApp.Models;
using Microsoft.AspNet.Identity;

namespace ContactsCRUDApp.Controllers
{
    public class ContactsController : Controller
    {
        //(Note: We are NOT using dependency injection here for the DB context, although it would be a good idea.)
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        [Authorize]
        public ActionResult Index()
        {
            var userId = GetCurrentUserGuid();
            return View(db.Contacts.Where(x => x.UserId == userId).ToList());
        }

        // GET: Contacts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,MyProperty,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,Birthday,StressAddress1,StressAddress2,City,State,Zip")] Contact contact)
        {
            contact.UserId = GetCurrentUserGuid(); //Set's the contacts Guid to be the current logged in user's Guid.  (One to many relationship between logged in user to contacts)
                                                   //This is to ensure that each user can only edit and see contacts with a matching Guid.
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,MyProperty,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,Birthday,StressAddress1,StressAddress2,City,State,Zip")] Contact contact)
        {
            contact.UserId = GetCurrentUserGuid();
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //This function is used to find the current logged in user's ID.
        //We can then use that information to restrict or grant access to certain pages.
        public Guid GetCurrentUserGuid()
        {
            return new Guid(User.Identity.GetUserId());
        }

        //Ensures that the user is allowed to update the contact.
        private bool EnsureIsUserContact(Contact contact)
        {
            return contact.UserId == GetCurrentUserGuid();
        }
    }
}
