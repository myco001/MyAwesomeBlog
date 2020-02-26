using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyAwesomeBlog.Models;
using Microsoft.AspNet.Identity;

namespace MyAwesomeBlog.Controllers
{
    public class CommentsController : Controller
    {
        private Blog_Models db = new Blog_Models();

        // GET: Comments/Create
        public ActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,CommentContent,PostId")] Comment comment)
        {
            comment.UserId = User.Identity.GetUserId();
            comment.CommentDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            ViewBag.PostId = comment.PostId;
            return View(comment);
        }
    }
}
