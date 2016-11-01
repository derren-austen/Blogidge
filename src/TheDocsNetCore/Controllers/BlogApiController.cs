using Microsoft.AspNetCore.Mvc;
using TheDocsNetCore.Models;
using TheDocsNetCore.Data;
using TheDocsNetCore.Models.BlogidgeViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheDocsNetCore.Controllers
{
    [Route("api/[controller]")]
    public class BlogApiController : Controller
    {
        private readonly IPostRepo _postRepo;

        public BlogApiController (IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        // GET: api/values
        [HttpGet]
        public PostApiViewModel Get ()
        {
            Post post = _postRepo.GetPost(18);

            PostApiViewModel viewModel = new PostApiViewModel
            {
                PostID = post.PostID,
                Title = post.Title,
                Content = post.Content,
                Author = post.User.FirstName,
                Created = post.Created,
                Updated = post.Updated,
                ReleaseDate = post.ReleaseDate.Value
                //Series = post.Series
            };

            return viewModel;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
