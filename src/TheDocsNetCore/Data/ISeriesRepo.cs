using System.Collections.Generic;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.Data
{
    public interface ISeriesRepo
    {
        List<Post> GetAllPostsInSeries (int seriesID);

        List<Series> GetAllSeries ();
    }
}
