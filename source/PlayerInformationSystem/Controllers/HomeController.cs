using PlayerInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlayerInformationSystem.Controllers
{
    public class HomeController : Controller
    {
        private PlayerInformationSystemEntities _dbContext = new PlayerInformationSystemEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var userId = _dbContext.Users.Where(u => u.Username == user.Username && u.Password == user.Password).Select(x => x.UserId).FirstOrDefault();
                var roleId = _dbContext.UserRoles.Where(u => u.UserId == userId).Select(x=>x.RoleId).FirstOrDefault();
                var roleName = _dbContext.Roles.Where(u => u.RoleId == roleId).Select(x => x.RoleName).FirstOrDefault();

                if (userId != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["UserId"] = user.UserId;
                    Session["Username"] = user.Username;
                    Session["Email"] = user.Email;
                    Session["Rolename"] = roleName;
                    return RedirectToAction("UserDashBoard");
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult UserDashBoard()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}