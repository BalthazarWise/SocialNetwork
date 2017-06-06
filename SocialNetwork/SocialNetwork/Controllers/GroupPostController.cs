using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class GroupPostController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: GroupPost
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            
            var gP = context.Groups.Where(e => e.Users.Any(u => u.Id == userId)).SelectMany(p => p.GroupPosts).ToList();

            return View(gP);
        }
        [HttpPost]
        public ActionResult Add(GroupPost add, int id)
        {
            if (ModelState.IsValid)
            {
                add.Group = context.Groups.Find(id);
                context.GroupPosts.Add(add);
                context.SaveChanges();

                return RedirectToAction("Group", "Group", new { id });
            }
            return View(add);
        }
        #region Not implementation
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            GroupPost groupPost = context.GroupPosts.Find(id);
            if (groupPost != null)
            {
                return View(groupPost);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(GroupPost edited)
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
                context.GroupPosts.Remove(context.GroupPosts.Find(id));
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Такой пост не существует");
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
        #endregion
    }
}