using DbRepos;
using Models;
namespace Services
{

    public class CommentServiceDb : ICommentService
    {
        private readonly CommentDbRepos _repo = null;
        public Task<List<IComments>> ReadCommentsAsync(int number)
        {
            return _repo.ReadCommentsAsync(number);
        }
        public CommentServiceDb(CommentDbRepos repo)
        {
            _repo = repo;
        }
    }
}