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
    public class PostsController : Controller
    {
        private Blog_Models db = new Blog_Models();

        // GET: Posts
        [HttpGet]
        public ActionResult Index()
        {
            List<APost> posts = new List<APost>();
            ApplicationDbContext Context = new ApplicationDbContext();

            foreach (Post post in db.Posts)
            {
                APost aPost = new APost
                {
                    PostId = post.PostId,
                    PostHeading = post.PostHeading,
                    UserId = post.UserId,
                    UserName = Context.Users.Find(post.UserId).UserName,
                    PostDate = post.PostDate,
                    PostContent = post.PostContent
                };
                posts.Add(aPost);
            }

            posts.Reverse();

            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            ApplicationDbContext Context = new ApplicationDbContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            List<AComment> comments = new List<AComment>();

            foreach(var item in post.Comments)
            {
                AComment aComment = new AComment
                {
                    CommentId = item.CommentId,
                    CommentContent = item.CommentContent,
                    CommentDate = item.CommentDate,
                    PostId = item.PostId
                };
                if ( item.UserId != null)
                {
                    aComment.UserName = Context.Users.Find(post.UserId).UserName;
                }
                else
                {
                    aComment.UserName = "Anonymous User";
                }
                comments.Add(aComment);
            }

            APost postComments = new APost
            {
                PostId = post.PostId,
                PostHeading = post.PostHeading,
                UserId = post.UserId,
                UserName = Context.Users.Find(post.UserId).UserName,
                PostDate = post.PostDate,
                PostContent = post.PostContent,
                Comments = comments
            };
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(postComments);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PostId,PostHeading,PostContent")] Post post)
        {
            post.UserId = User.Identity.GetUserId();
            post.PostDate = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(post);

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else if (User.IsInRole("Admin") || post.UserId == User.Identity.GetUserId())
            {
                return View(post);
            }
            return RedirectToAction("Warning");
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PostId,UserId,PostHeading,PostContent,PostDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else if (User.IsInRole("Admin") || post.UserId == User.Identity.GetUserId())
            {
                return View(post);
            }
            return RedirectToAction("Warning");
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Warning()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
