using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace PlayerInformationSystem.Controllers
{
    public class PlayersController : Controller
    {
        private PlayerInformationSystemEntities db = new PlayerInformationSystemEntities();

        [Authorize(Roles = "Admin,Player,Committee")]
        public ActionResult Index()
        {
            if (Session["Rolename"].ToString() == "Committee")
            {
                var allPlayers = db.Players.Where(m => m.IsActive == false).Include(p => p.Club).Include(p => p.Gender).Include(p => p.Position);
                return View(allPlayers.ToList());
            }
            else 
            {
                var activePlayers = db.Players.Where(m => m.IsActive == true).Include(p => p.Club).Include(p => p.Gender).Include(p => p.Position);
                return View(activePlayers.ToList());
            }            
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Player,Committee")]
        public ActionResult Index(string sortOrder, string orderBy, string playerNumber, string playerName, string age, 
            DateTime? hireDate, DateTime? expiredDate, string clubName, string gender, string position)
        {

            var playerList = from p in db.Players
                             join c in db.Clubs on p.ClubId equals c.ClubId
                             join g in db.Genders on p.GenderId equals g.GenderId
                             join pos in db.Positions on p.PositionId equals pos.PositionId
                             where (string.IsNullOrEmpty(playerNumber) || p.PlayerNumber.Contains(playerNumber)) 
                             && (string.IsNullOrEmpty(playerName) || p.Name.Contains(playerName)) 
                             && (string.IsNullOrEmpty(age) || p.Age.ToString().Contains(age))
                             && (!hireDate.HasValue || p.HireDate >= hireDate.Value)
                             && (!expiredDate.HasValue || p.ExpiredDate <= expiredDate.Value)
                             && (string.IsNullOrEmpty(clubName) || c.ClubName.Contains(clubName))
                             && (string.IsNullOrEmpty(gender) || g.Name.Contains(gender))
                             && (string.IsNullOrEmpty(position) || pos.Name.Contains(position))
                             orderby p.PlayerNumber descending
                             select p;

            if (Session["Rolename"].ToString() == "Committee")
            {
                playerList = playerList.Where(p => p.IsActive == false);
            }
            else 
            {
                playerList = playerList.Where(p => p.IsActive == true);
            }

            if (sortOrder == "PlayerNumber")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.PlayerNumber);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.PlayerNumber);
                        break;
                }

            }
            else if (sortOrder == "PlayerName")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.Name);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.Name);
                        break;
                }
            }
            else if (sortOrder == "Age")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.Age);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(s => s.Age);
                        break;
                }
            }
            else if (sortOrder == "HireDate")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.HireDate);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.HireDate);
                        break;
                }
            }
            else if (sortOrder == "ExpiredDate")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(s => s.ExpiredDate);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(s => s.ExpiredDate);
                        break;
                }
            }
            else if (sortOrder == "ClubName")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.Club.ClubName);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.Club.ClubName);
                        break;
                }
            }
            else if (sortOrder == "GenderName")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.Gender.Name);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.Gender.Name);
                        break;
                }
            }
            else if (sortOrder == "PositionName")
            {
                switch (orderBy)
                {
                    case "Ascending":
                        playerList = playerList.OrderBy(p => p.Position.Name);
                        break;
                    case "Descending":
                        playerList = playerList.OrderByDescending(p => p.Position.Name);
                        break;
                }
            }

            return View(playerList.ToList());
        }

        [Authorize(Roles = "Admin,Player,Committee")]
        public ActionResult Details(int? id)
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
                player.PlayerNumber = Util.GetPlayerNumber(player.PositionId);
                player.CreatedTime = DateTime.Now;
                player.CreatedBy = Session["Username"].ToString();
                player.IsActive = false;
                db.Players.Add(player);

                TaskApproval task = new TaskApproval();
                task.PlayerNumber = player.PlayerNumber;
                task.IsClosed = false;
                task.Status = "OnProgress";
                task.CreatedTime = DateTime.Now;
                task.CreatedBy = Session["Username"].ToString();
                task.Owner = "Committee";
                db.TaskApprovals.Add(task);

                db.SaveChanges();

                TempData["pesan"] = "Your registered successfully with Player Number : " + player.PlayerNumber;

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
            Player player = db.Players.Find(id);
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

                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Player player = db.Players.Find(id);
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
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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

        [Authorize(Roles = "Committee")]
        public ActionResult Approve(int? id)
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
                var player = db.Players.Where(m => m.PlayerId == task.playerId).FirstOrDefault();
                if (player != null)
                {
                    player.IsActive = true;
                    player.UpdatedTime = DateTime.Now;
                    player.UpdatedBy = Session["Username"].ToString();

                    var lastTask = db.TaskApprovals.Where(t => t.PlayerNumber == player.PlayerNumber && t.Owner == "Committee").FirstOrDefault();
                    if (lastTask != null)
                    {
                        lastTask.UpdatedTime = DateTime.Now;
                        lastTask.UpdatedBy = Session["Username"].ToString();
                        lastTask.Status = "Approved";
                        lastTask.Note = task.note;
                        lastTask.IsClosed = true;
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
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

        //[Authorize(Roles = "Releaser")]
        //public ActionResult Release(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Player player = db.Players.Find(id);
        //    if (player == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var approval = db.TaskApprovals.Where(a => a.PlayerNumber == player.PlayerNumber && a.Owner == "Releaser" && a.IsClosed == false).FirstOrDefault();

        //    ApprovalModel task = new ApprovalModel();
        //    task.playerNumber = player.PlayerNumber;
        //    task.playerName = player.Name;
        //    task.club = player.Club.ClubName;
        //    task.position = player.Position.Name;
        //    task.approvedBy = approval.CreatedBy;
        //    task.approvedDate = approval.CreatedTime;
        //    task.hireDate = player.HireDate;
        //    task.expiredDate = player.ExpiredDate;
        //    task.note = approval.Note;

        //    return View(task);
        //}

        //[Authorize(Roles = "Releaser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Release(ApprovalModel task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var player = db.Players.Where(m => m.PlayerNumber == task.playerNumber).FirstOrDefault();
        //        player.IsActive = true;

        //        var lastTask = db.TaskApprovals.Where(t => t.PlayerNumber == task.playerNumber && t.Owner == "Releaser" && t.IsClosed == false).FirstOrDefault();
        //        if (lastTask != null)
        //        {
        //            lastTask.UpdatedTime = DateTime.Now;
        //            lastTask.UpdatedBy = Session["Username"].ToString();
        //            lastTask.Status = "Released";
        //            lastTask.Note = task.note;
        //            lastTask.IsClosed = true;

        //            db.SaveChanges();
        //        }
        //    }

        //    return View();
        //}
    }
}
