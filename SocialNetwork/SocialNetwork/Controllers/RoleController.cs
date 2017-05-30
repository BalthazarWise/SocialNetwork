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
    [Authorize]
    public class RoleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.OrderBy(x=>x.Name).ToList());
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(string roleName)
        {
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
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
            return View(db.Roles.Find(roleName));
        }
        [HttpPost]
        public ActionResult EditRole(IdentityRole role)
        {
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!manager.RoleExists(role.Name))
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Content("Такая роль уже существует");
        }
        [HttpGet]
        public ActionResult DeleteRole(string roleName)
        {
            try
            {
                db.Roles.Remove(db.Roles.Find(roleName));
                db.SaveChanges();
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
    }
}