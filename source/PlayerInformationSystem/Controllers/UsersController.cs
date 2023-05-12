using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using PlayerInformationSystem.Repository;

namespace PlayerInformationSystem.Controllers
{
    public class UsersController : Controller
    {
        #region Constructor  
        UserRepository userRepo;
        PlayerInformationSystemEntities db;
        public UsersController()
        {
            userRepo = new UserRepository();
            db = new PlayerInformationSystemEntities();
        }
        #endregion


        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Add ViewBag to save SortOrder of table
            ViewBag.Username = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.Email = sortOrder == "email" ? "email_desc" : "email";

            //he search string is changed when a value is entered in the text box and the submit button is pressed. In that case, the searchString parameter is not null.
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //ViewBag.CurrentFilter, provides the view with the current filter string.            
            ViewBag.CurrentFilter = searchString;

            var listUser = userRepo.GetDataByFilter(sortOrder, searchString);
            
            //indicates the size of list
            string pSize = ConfigurationManager.AppSettings["PageSize"];
            int pageSize = Convert.ToInt32(pSize);
            //set page to one is there is no value, ??  is called the null-coalescing operator.
            int pageNumber = (page ?? 1);
            //return the Model data with paged
            return View(listUser.ToPagedList(pageNumber, pageSize));

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
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel registerUser)
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name");

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
                //using (var db = new PlayerInformationSystemEntities())
                //{
                    db.Dispose();
                //}
            }
            base.Dispose(disposing);
        }
    }
}
