using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using PlayerInformationSystem.Repository;

namespace PlayerInformationSystem.Controllers
{
    public class UsersController : Controller
    {
        #region Constructor  
        UserRepository userRepo;
        public UsersController()
        {
            userRepo = new UserRepository();
        }
        #endregion

        // GET: Users
        public ActionResult Index()
        {
            return View(userRepo.GetAllData());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = userRepo.GetDataById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            using (var db = new PlayerInformationSystemEntities())
            {
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
                ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
                ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name");
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name");
            }            
            
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = userRepo.Insert(registerUser);

                if (user.RoleName == "Player")
                {
                    TempData["pesan"] = "Your registered successfully with Player Number : "+ user.PlayerNumber + ". Please click Login here";
                }
                else 
                {
                    TempData["pesan"] = "Your registered successfully. Please click Login here";
                }                    

                return RedirectToAction("Create");

            }
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = userRepo.GetDataById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                string message = userRepo.Update(user);

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = userRepo.GetDataById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userRepo.Delete(id);
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
