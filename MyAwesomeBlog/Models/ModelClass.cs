using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAwesomeBlog.Models
{
    public class APost
    {
        public int PostId { get; set; }
        public string PostHeading { get; set; }
        public DateTime PostDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PostContent { get; set; }
        public List<AComment> Comments { get; set; }
        public Comment NewComment { get; set; }
    }

    public class AComment
    {
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
    }
}