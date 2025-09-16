using DbRepos;
using Models;
namespace Services
{

    public class CommentService : ICommentService
    {
        private readonly CommentService _repo = null;
        public Task<List<IComments>> ReadCommentsAsync(int number)
        {
            return _repo.ReadCommentsAsync(number);
        }
        public CommentService(CommentService repo)
        {
            _repo = repo;
        }
    }
}