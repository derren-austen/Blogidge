using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheDocsNetCore.Models
{
    public class Comment
    {
        public Comment ()
        {
            Status = CommentStatus.Unapproved;
            Updated = DateTime.Now;
        }

        public int CommentID { get; set; }
        [Required]
        public string Content { get; set; }
        public CommentStatus Status { get; set; }
        [Required, MaxLength(450)]
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }
        [ForeignKey("Author")]
        public ApplicationUser User { get; set; }
    }

    public enum CommentStatus
    {
        Approved = 1,
        Unapproved = 2,
        Rejected = 3
    }
}
