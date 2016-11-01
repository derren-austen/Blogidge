using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Net;
using TheDocsNetCore.Data;
using TheDocsNetCore.Extensions;
using TheDocsNetCore.Models;
using TheDocsNetCore.Models.BlogidgeViewModels;

namespace TheDocsNetCore.Controllers
{
    [Authorize(Policy = "Editidge")]
    public class PostsController : Controller
    {
        private readonly IPostRepo _postRepo;
        private readonly ISeriesRepo _seriesRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController (IPostRepo postRepo, ISeriesRepo seriesRepo, UserManager<ApplicationUser> userManager)
        {
            _postRepo = postRepo;
            _seriesRepo = seriesRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult New ()
        {
            ViewData["StatusSelect"] = new SelectList(Enum.GetNames(typeof(PostStatus)));
            ViewData["SeriesSelect"] = new SelectList(_seriesRepo.GetAllSeries(), "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New ([Bind("Title", "Slug", "MarkDownContent", "Status", "ReleaseDate", "SeriesID")]PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                string userID = _userManager.GetUserId(User);

                Post post = new Post
                {
                    Author = userID,
                    Title = postViewModel.Title,
                    Slug = postViewModel.Slug,
                    Created = DateTime.Now,
                    MarkDownContent = postViewModel.MarkDownContent,
                    Content = postViewModel.MarkDownContent.ToHTML(),
                    ReleaseDate = postViewModel.ReleaseDate,
                    Status = postViewModel.Status,
                    SeriesID = postViewModel.SeriesID == 0 ? null : postViewModel.SeriesID
            };

                int newPostID = _postRepo.CreatePost(post);

                return RedirectToAction("Index", "Blog", new { id = newPostID, slug = postViewModel.Slug });
            }

            return View(postViewModel);
        }

        [HttpGet]
        public IActionResult Update (int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            Post post = _postRepo.GetPost(id);

            if (post == null)
            {
                return NotFound("Sorry, this post cannot be found");
            }

            ViewData["StatusSelect"] = new SelectList(Enum.GetNames(typeof(PostStatus)), post.Status);
            ViewData["SeriesSelect"] = new SelectList(_seriesRepo.GetAllSeries(), "ID", "Name");

            PostViewModel postViewModel = new PostViewModel
            {
                PostID = post.PostID,
                Title = post.Title,
                Slug = post.Slug,
                MarkDownContent = post.MarkDownContent,
                Status = post.Status,
                ReleaseDate = post.ReleaseDate,
                SeriesID = post.SeriesID
            };

            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update ([Bind("PostID", "Title", "Slug", "MarkDownContent", "Status", "ReleaseDate", "SeriesID")]PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                string userID = _userManager.GetUserId(User);

                Post post = _postRepo.GetPost(postViewModel.PostID);

                post.Author = userID;
                post.Title = postViewModel.Title;
                post.Slug = postViewModel.Slug;
                post.Updated = DateTime.Now;
                post.MarkDownContent = postViewModel.MarkDownContent;
                post.Content = postViewModel.MarkDownContent.ToHTML();
                post.ReleaseDate = postViewModel.ReleaseDate;
                post.Status = postViewModel.Status;
                post.SeriesID = postViewModel.SeriesID == 0 ? null : postViewModel.SeriesID;

                _postRepo.UpdatePost(post);
                
                return RedirectToAction("Index", "Blog", new { id = postViewModel.PostID, slug = postViewModel.Slug });
            }

            return View(postViewModel);
        }
    }
}
