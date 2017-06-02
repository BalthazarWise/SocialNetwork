using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Group
        public ActionResult Index()
        {
            IEnumerable<Group> groups = context.Groups.OrderByDescending(x => x.Id);
            return View(groups);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Group add)
        {
            if (ModelState.IsValid)
            {
                context.Groups.Add(add);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(add);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Group group = context.Groups.Find(id);
            if (group != null)
            {
                return View(group);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Group edited)
        {
            if (ModelState.IsValid)
            {
                context.Entry(edited).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(edited);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                context.Groups.Remove(context.Groups.Find(id));
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

    }
}