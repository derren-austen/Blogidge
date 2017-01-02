using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.Data
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public PostRepo (ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public int CreatePost (Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();

            return post.PostID;
        }

        public void UpdatePost (Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Post> GetAllPosts (int page, bool userAuthenticated = false)
        {
            List<Post> posts;

            int pageNo = 0;
            if (page > 0) pageNo = page - 1;

            if (userAuthenticated)
                posts = _context.Posts
                                .Include(p => p.User)
                                .Include(s => s.Series)
                                .Where(p => p.ReleaseDate <= DateTime.Now && (p.Status == PostStatus.Private || p.Status == PostStatus.Public))
                                .OrderByDescending(p => p.ReleaseDate)
                                .Skip(pageNo * _appSettings.PostsPerPage)
                                .Take(_appSettings.PostsPerPage)
                                .ToList();
            else
                posts = _context.Posts
                                .Include(p => p.User)
                                .Include(s => s.Series)
                                .Where(p => p.ReleaseDate <= DateTime.Now && p.Status == PostStatus.Public)
                                .OrderByDescending(p => p.ReleaseDate)
                                .Skip(pageNo * _appSettings.PostsPerPage)
                                .Take(_appSettings.PostsPerPage)
                                .ToList();

            return (posts);
        }

        public Post GetLatestPost (bool userAuthenticated = false)
        {
            Post post;

            if (userAuthenticated)
                post = _context.Posts
                               .Include(p => p.User)
                               .Include(s => s.Series)
                               .Where(p => p.ReleaseDate <= DateTime.Now && (p.Status == PostStatus.Private || p.Status == PostStatus.Public))
                               .OrderByDescending(p => p.ReleaseDate)
                               .OrderByDescending(p => p.PostID)
                               .FirstOrDefault();
            else
                post = _context.Posts
                               .Include(p => p.User)
                               .Include(s => s.Series)
                               .Where(p => p.ReleaseDate <= DateTime.Now && p.Status == PostStatus.Public)
                               .OrderByDescending(p => p.ReleaseDate)
                               .OrderByDescending(p => p.PostID)
                               .FirstOrDefault();

            return (post);
        }

        public Post GetPost (int? postID)
        {
            Post post = _context.Posts
                                .Include(p => p.User)
                                .Include(s => s.Series)
                                .Where(p => p.PostID == postID)
                                .FirstOrDefault();

            return post;
        }
    }
}
