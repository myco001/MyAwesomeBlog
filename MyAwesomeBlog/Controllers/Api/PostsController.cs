using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyAwesomeBlog.Models;

namespace MyAwesomeBlog.Controllers.Api
{
    public class PostsController : ApiController
    {
        private Blog_Models db = new Blog_Models();

        // GET /api/posts
        public IEnumerable<Post> GetPosts()
        {
            return db.Posts.ToList();
        }

        // GET /api/posts/1
        public Post GetPost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return post;
        }

        // POST /api/posts
        [HttpPost]
        public Post CreatePost(Post post)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            db.Posts.Add(post);
            db.SaveChanges();

            return post;
        }

        // PUT /api/posts/1
        [HttpPut]
        public void UpdatePost(int id, Post post)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var postInDb = db.Posts.SingleOrDefault(p => p.PostId == id);

            if (postInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            postInDb.PostHeading = post.PostHeading;
            postInDb.PostContent = post.PostContent;

            db.SaveChanges();
        }

        // DELETE /api/posts/1
        [HttpDelete]
        public void DeletePost(int id)
        {
            var postInDb = db.Posts.SingleOrDefault(p => p.PostId == id);

            if (postInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
        }
    }
}
