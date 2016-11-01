using System.Collections.Generic;
using TheDocsNetCore.Models;

namespace TheDocsNetCore.Data
{
    public interface IPostRepo
    {
        List<Post> GetAllPosts (bool userAuthenticated = false);

        Post GetPost (int? postID);

        Post GetLatestPost (bool userAuthenticated = false);

        int CreatePost (Post post);

        void UpdatePost (Post post);
    }
}
