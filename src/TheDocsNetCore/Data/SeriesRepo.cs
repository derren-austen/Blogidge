using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.Data
{
    public class SeriesRepo : ISeriesRepo
    {
        private readonly ApplicationDbContext _context;

        public SeriesRepo (ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPostsInSeries (int seriesID)
        {
            List<Post> posts = _context.Posts
                                       .Include(p => p.User)
                                       .Include(s => s.Series)
                                       .Where(p => p.SeriesID == seriesID && p.ReleaseDate <= DateTime.Now)
                                       .Where(p => p.Status == PostStatus.Public)
                                       .OrderByDescending(p => p.ReleaseDate)
                                       .ToList();

            return (posts);
        }

        public List<Series> GetAllSeries ()
        {
            List<Series> series = _context.Series.ToList();

            return series;
        }
    }
}
