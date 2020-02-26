namespace MyAwesomeBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Comment
    {
        public int CommentId { get; set; }

        public string UserId { get; set; }

        [Required]
        [AllowHtml]
        public string CommentContent { get; set; }

        public DateTime CommentDate { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
