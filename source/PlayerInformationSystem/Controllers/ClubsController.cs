using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using PagedList;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using PlayerInformationSystem.Repository;

namespace PlayerInformationSystem.Controllers
{
    public class ClubsController : Controller
    {
        #region Constructor  
        ClubRepository clubRepo;
        public ClubsController()
        {
            clubRepo = new ClubRepository();
        }
        #endregion

        //public ActionResult Index()
        //{
        //    return View(clubRepo.GetAllData());
        //}

        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Add ViewBag to save SortOrder of table
            ViewBag.ClubName = String.IsNullOrEmpty(sortOrder) ? "clubname_desc" : "";

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

            var listPlayers = clubRepo.GetDataClub(sortOrder, searchString);

            //indicates the size of list
            string pSize = ConfigurationManager.AppSettings["PageSize"];
            int pageSize = Convert.ToInt32(pSize);
            //set page to one is there is no value, ??  is called the null-coalescing operator.
            int pageNumber = (page ?? 1);
            //return the Model data with paged
            return View(listPlayers.ToPagedList(pageNumber, pageSize));

        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = clubRepo.GetDataById(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // GET: Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Club club)
        {
            if (ModelState.IsValid)
            {
                string message = clubRepo.Insert(club);
                
                return RedirectToAction(message);
            }

            return View(club);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = clubRepo.GetDataById(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Club club)
        {
            if (ModelState.IsValid)
            {
                string message = clubRepo.Update(club);
                return RedirectToAction(message);
            }
            return View(club);
        }

        // GET: Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = clubRepo.GetDataById(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            clubRepo.Delete(id);
            return RedirectToAction("Index");
        }
        //public JsonResult GetClubs(string search)
        //{
        //    var clubs = clubRepo.GetClubName(search);
        //    return new JsonResult { Data = clubs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public JsonResult GetClubs(string search)
        {
            List<AutoCompleteModel> allsearch = clubRepo.GetClubName(search);

            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
