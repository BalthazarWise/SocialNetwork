using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    [Authorize (Roles = "Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(context.Roles.OrderBy(x => x.Name).ToList());
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(string roleName)
        {
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!manager.RoleExists(roleName))
            {
                IdentityRole role = new IdentityRole(roleName);
                manager.Create(role);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditRole(string roleName)
        {
            return View(context.Roles.Find(roleName));
        }
        [HttpPost]
        public ActionResult EditRole(IdentityRole role)
        {
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!manager.RoleExists(role.Name))
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Content("Такая роль уже существует");
        }
        [HttpGet]
        public ActionResult DeleteRole(string roleName)
        {
            try
            {
                context.Roles.Remove(context.Roles.Find(roleName));
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Такая роль не существует");
            }

            //RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //if (manager.RoleExists(roleName))
            //{
            //    db.Roles.Remove(db.Roles.Find(roleName));
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return Content("Такая роль не существует");
        }
        [HttpGet]
        public ActionResult SetUserRole()
        {
            return View(context.Users.OrderBy(x => x.UserName).ToList());
        }
        [HttpPost]
        public ActionResult SetUserRole(string roleName, string userId, string action)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            switch (action)
            {
                case "Add Role":
                    {
                        userManager.AddToRole(userId, roleName);
                        break;
                            
                    }
                case "Remove Role":
                    {
                        userManager.RemoveFromRole(userId, roleName);
                        break;
                    }
                default:
                    break;
            }
            ;
            return RedirectToAction("SetUserRole");
        }
    }
}