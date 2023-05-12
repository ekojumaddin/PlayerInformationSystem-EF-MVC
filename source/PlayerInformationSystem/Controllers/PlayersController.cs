using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using PlayerInformationSystem.Repository;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace PlayerInformationSystem.Controllers
{
    public class PlayersController : Controller
    {
        #region Constructor  
        PlayerRepository playerRepo;
        public PlayersController()
        {
            playerRepo = new PlayerRepository();
        }
        #endregion

        private PlayerInformationSystemEntities db = new PlayerInformationSystemEntities();

        [Authorize(Roles = "Admin,Player,Committee")]
        public ViewResult Index(string sortOrder, DateTime? joinDate, DateTime? expireDate, string currentFilter, string searchString, int? page)
        {
            //ViewBag to save Order by column
            ViewBag.PlayerNumber = String.IsNullOrEmpty(sortOrder) ? "playernumber_desc" : "";
            ViewBag.PlayerName = sortOrder == "playername" ? "playername_desc" : "playername";
            ViewBag.Age = sortOrder == "age" ? "age_desc" : "age";
            ViewBag.HireDate = sortOrder == "hiredate" ? "hiredate_desc" : "hiredate";
            ViewBag.ExpiredDate = sortOrder == "expiredate" ? "expiredate_desc" : "expiredate";
            ViewBag.ClubName = sortOrder == "clubname" ? "clubname_desc" : "clubname";
            ViewBag.PositionName = sortOrder == "position" ? "position_desc" : "position";

            //search string is changed when a value is entered in the text box and the submit button is pressed. In that case, the searchString parameter is not null.
            if (searchString != null || joinDate != null || expireDate != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //provides the view with the current filter string.            
            ViewBag.CurrentFilter = searchString;

            var listPlayers = playerRepo.GetDataPlayer(sortOrder, searchString, joinDate, expireDate);

            if (Session["Rolename"].ToString() == "Committee")
            {
                listPlayers.Where(p => p.IsActive == false);
            }
            else if (Session["Rolename"].ToString() == "Player")
            {
                listPlayers.Where(p => p.IsActive == true);
            }

            //indicates the size of list
            string stringPageSize = ConfigurationManager.AppSettings["PageSize"];
            int pageSize = Convert.ToInt32(stringPageSize);
            int pageNumber = (page ?? 1);

            //return the Model data with paged
            return View(listPlayers.ToPagedList(pageNumber, pageSize));

        }

        [Authorize(Roles = "Admin,Player,Committee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetDataById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name");
            return View();
        }

        [Authorize(Roles = "Admin, Player")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                player.CreatedBy = Session["Username"].ToString();
                string message = playerRepo.Insert(player);

                TempData["pesan"] = message;

                return RedirectToAction("Create");
            }

            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name", player.GenderId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name", player.PositionId);
            return View(player);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetDataById(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name", player.GenderId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name", player.PositionId);
            return View(player);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                player.UpdatedTime = DateTime.Now;
                player.UpdatedBy = Session["Username"].ToString();

                string message = playerRepo.Update(player);
                return RedirectToAction(message);
            }

            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "Name", player.GenderId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Name", player.PositionId);
            return View(player);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetDataById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            playerRepo.Delete(id);
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

        [Authorize(Roles = "Committee")]
        public ActionResult Approve(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetDataById(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            ApprovalModel task = new ApprovalModel();
            task.playerId = player.PlayerId;
            task.playerNumber = player.PlayerNumber;
            task.playerName = player.Name;
            task.club = player.Club.ClubName;
            task.position = player.Position.Name;
            task.hireDate = player.HireDate;
            task.expiredDate = player.ExpiredDate;

            return View(task);
        }

        [Authorize(Roles = "Committee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(ApprovalModel task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedBy = Session["Username"].ToString();
                string message = playerRepo.Approve(task);               

                return RedirectToAction(message);
            }

            return View();
        }

        [Authorize(Roles = "Committee")]
        public ActionResult Reject(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            ApprovalModel task = new ApprovalModel();
            task.playerNumber = player.PlayerNumber;
            task.playerName = player.Name;
            task.club = player.Club.ClubName;
            task.position = player.Position.Name;
            return View(task);
        }

        [Authorize(Roles = "Committee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(ApprovalModel task)
        {
            if (ModelState.IsValid)
            {
                var lastTask = db.TaskApprovals.Where(t => t.PlayerNumber == task.playerNumber).FirstOrDefault();
                if (lastTask != null)
                {
                    lastTask.UpdatedTime = DateTime.Now;
                    lastTask.UpdatedBy = Session["Username"].ToString();
                    lastTask.IsClosed = true;
                    lastTask.Status = "Rejected";
                }

                TaskApproval taskApproval = new TaskApproval();
                taskApproval.Note = task.note;
                taskApproval.Owner = "Releaser";
                taskApproval.Status = "Rejected";
                taskApproval.PlayerNumber = task.playerNumber;
                taskApproval.CreatedTime = DateTime.Now;
                taskApproval.CreatedBy = Session["Username"].ToString();
                taskApproval.IsClosed = true;

                db.TaskApprovals.Add(taskApproval);
                db.SaveChanges();
            }

            return View();
        }
    }
}
