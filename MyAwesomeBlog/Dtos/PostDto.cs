using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyAwesomeBlog.Models;

namespace MyAwesomeBlog.Dtos
{
    public class PostDto
    {
        //public Post()
        //{
        //    Comments = new HashSet<Comment>();
        //}

        public int PostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string PostHeading { get; set; }

        [Required]
        [AllowHtml]
        public string PostContent { get; set; }

        public DateTime PostDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}