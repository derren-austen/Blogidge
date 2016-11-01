using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheDocsNetCore.Data;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.ViewComponents
{
    [ViewComponent(Name = "PostsInSeries")]
    public class PostsInSeries : ViewComponent
    {
        private readonly ISeriesRepo _seriesRepo;

        public PostsInSeries (ISeriesRepo seriesRepo)
        {
            _seriesRepo = seriesRepo;
        }

        public IViewComponentResult Invoke (int seriesID, int? postID)
        {
            List<Post> postsInSeries = _seriesRepo.GetAllPostsInSeries(seriesID);

            if(postID != null) 
                ViewData["ThisOne"] = postID;

            return View(postsInSeries);
        }
    }
}
