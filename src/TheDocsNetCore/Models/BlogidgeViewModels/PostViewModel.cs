using System;
using System.ComponentModel.DataAnnotations;

namespace TheDocsNetCore.Models.BlogidgeViewModels
{
    public class PostViewModel
    {
        public int? PostID { get; set; }
        [Required(ErrorMessage = "I'm gonna be needing a title for this!"), MaxLength(200, ErrorMessage = "The title can only be 200 characters, sorry!")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "I'm gonna be needing a slug too!"), MaxLength(220, ErrorMessage = "The slug can only be 220 characters, sorry!")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "How about you write some content!")]
        [Display(Name = "Content")]
        public string MarkDownContent { get; set; }
        public string Content { get; set; }
        [Required]
        [Display(Name = "Status")]
        public PostStatus Status { get; set; }
        [Display(Name = "Updated by")]
        public string AuthorEmail { get; set; }
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        [MaxLength(11)]
        [Display(Name = "YouTube video ID")]
        public string YouTubeVideoID { get; set; }
        [Display(Name = "Series")]
        public int? SeriesID { get; set; }
        [Display(Name = "Teaser text")]
        public string MarkDownTeaserText { get; set; }
        public string TeaserText { get; set; }
    }
}
