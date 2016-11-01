using System;
using System.Collections.Generic;

namespace TheDocsNetCore.Models.BlogidgeViewModels
{
    public class PostApiViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Series { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
