using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories
{
	public interface ICommentRepoistory
	{
		Task CreateAsync(CommentEntity comment);
		Task UpdateAsync(CommentEntity comment);
		Task<CommentEntity> GetByIdAsync(Guid commentId);
		Task DeleteAsync(Guid commentId);
	}
}