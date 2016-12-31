using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TheDocsNetCore.Data;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostRepo _postRepo;
        private readonly ISeriesRepo _seriesRepo;
        private readonly ILogger<BlogController> _logger;

        public BlogController (IPostRepo postRepo, ISeriesRepo seriesRepo, ILogger<BlogController> logger)
        {
            _postRepo = postRepo;
            _seriesRepo = seriesRepo;
            _logger = logger;
        }

        public IActionResult Index(int? id, string slug)
        {
            //TODO: abstract the claims level access stuff to a filter
            
            Post post;

            _logger.LogInformation(1, "Entered the Index action in the Home controller");

            if (id == null)
            {
                List<Post> posts = _postRepo.GetAllPosts(User.Identity.IsAuthenticated && (User.HasClaim("ViewPosts", "Viewer") || User.HasClaim("EditPosts", "Admin") || User.HasClaim("EditPosts", "Creator")));

                if (posts.Count == 0)
                {
                    ViewData["Oops"] = "There aren't any posts, dang!";

                    return View("Error");
                }

                return View("Home", posts);
            }

            post = _postRepo.GetPost(id);

            if (post == null)
            {
                ViewData["Oops"] = "Blog post not found.";

                return View("Error");
            }

            #region future release date

            if (post.ReleaseDate > DateTime.Now && !User.Identity.IsAuthenticated)
            {
                ViewData["Oops"] = "You must be logged in to view this future post.";

                return View("Error");
            }

            if (post.ReleaseDate > DateTime.Now && User.Identity.IsAuthenticated && !User.HasClaim("EditPosts", "Admin") && !User.HasClaim("EditPosts", "Creator"))
            {
                ViewData["Oops"] = "You must be logged in and posess the correct claims to view this future post.";

                return View("Error");
            }

            #endregion

            #region retired

            if (post.Status == PostStatus.Retired && !User.Identity.IsAuthenticated)
            {
                ViewData["Oops"] = "This post no longer exists.";

                return View("Error");
            }

            if (post.Status == PostStatus.Retired && User.Identity.IsAuthenticated && !User.HasClaim("EditPosts", "Admin") && !User.HasClaim("EditPosts", "Creator"))
            {
                ViewData["Oops"] = "This post has been retired and can only be viewed if you posess the correct claims.";

                return View("Error");
            }

            #endregion

            #region private or draft

            if ((post.Status == PostStatus.Private || post.Status == PostStatus.Draft) && !User.Identity.IsAuthenticated)
            {
                ViewData["Oops"] = "You must be logged in to view this post.";

                return View("Error");
            }

            if ((post.Status == PostStatus.Private || post.Status == PostStatus.Draft) && User.Identity.IsAuthenticated && !User.HasClaim("EditPosts", "Admin") && !User.HasClaim("EditPosts", "Creator"))
            {
                ViewData["Oops"] = "You must be logged in and posess the correct claims to view this post.";

                return View("Error");
            }

            #endregion

            return View(post);
        }

        public IActionResult AllPosts ()
        {
            List<Post> posts = _postRepo.GetAllPosts(User.Identity.IsAuthenticated && (User.HasClaim("ViewPosts", "Viewer") || User.HasClaim("EditPosts", "Admin") || User.HasClaim("EditPosts", "Creator")));

            return View(posts);
        }

        public IActionResult AllPostsInSeries (int? id)
        {
            if (id != null)
            {
                List<Post> postsInSeries = _seriesRepo.GetAllPostsInSeries(id.Value);

                return View(postsInSeries);
            }

            return View();
        }

        public IActionResult Error (int? id)
        {
            return View(id);
        }
    }
}
