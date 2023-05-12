using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Repository;

namespace PlayerInformationSystem.Controllers
{
    public class UserRolesController : Controller
    {
        #region Constructor  
        UserRoleRepository userRoleRepo;
        public UserRolesController()
        {
            userRoleRepo = new UserRoleRepository();
        }
        #endregion

        // GET: UserRoles
        public ActionResult Index()
        {
            return View(userRoleRepo.GetAllData());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = userRoleRepo.GetDataById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            using (var db = new PlayerInformationSystemEntities())
            {
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            }
            
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                string message = userRoleRepo.Insert(userRole);

                return RedirectToAction(message);
            }

            using (var db = new PlayerInformationSystemEntities())
            {
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userRole.RoleId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", userRole.UserId);
            }
            
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = userRoleRepo.GetDataById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }

            using (var db = new PlayerInformationSystemEntities())
            {
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userRole.RoleId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", userRole.UserId);
            }

            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                string message = userRoleRepo.Update(userRole);
                return RedirectToAction(message);
            }

            using (var db = new PlayerInformationSystemEntities())
            {
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userRole.RoleId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", userRole.UserId);
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = userRoleRepo.GetDataById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userRoleRepo.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (var db = new PlayerInformationSystemEntities())
                {
                    db.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
