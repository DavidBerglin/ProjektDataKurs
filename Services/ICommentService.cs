using Models;

namespace Services;

public interface ICommentService
{
   public Task<List<IComments>> ReadCommentsAsync(int number);
}