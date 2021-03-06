﻿using SocialNetwork.Models;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                return Content("Такая группа не существует");
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
        public ActionResult Group(int? id)
        {
            return View(context.Groups.Find(id));
        }
        [HttpGet]
        public ActionResult Subscribe(int? id, string userId)
        {
            context.Users.Find(userId).Groups.Add(context.Groups.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}