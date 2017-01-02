using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheDocsNetCore.Models
{
    public class Post
    {
        public Post ()
        {
            Status = PostStatus.Draft;
            Updated = DateTime.Now;
        }

        public int PostID { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required, MaxLength(220)]
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; }
        public string MarkDownContent { get; set; }
        [Required]
        public PostStatus Status { get; set; }
        [Required, MaxLength(450)]
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [MaxLength(11)]
        public string YouTubeVideoID { get; set; }
        public string TeaserText { get; set; }
        public string MarkDownTeaserText { get; set; }
        public int? SeriesID { get; set; }

        [ForeignKey("Author")]
        public ApplicationUser User { get; set; }
        public Series Series { get; set; }
    }

    public enum PostStatus
    {
        Draft = 1,
        Public = 2,
        Private = 3,
        Retired = 4
    }
}
