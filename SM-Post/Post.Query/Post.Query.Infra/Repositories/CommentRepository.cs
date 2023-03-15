using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infra.Data;

namespace Post.Query.Infra.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly DatabaseContextFactory _context;
		public CommentRepository(DatabaseContextFactory context)
		{
			_context = context;
		}

		public async Task CreateAsync(CommentEntity comment)
		{
			using DataContext context = _context.CreateDbContext();

			context.Comments.Add(comment);

			_ = await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid commentId)
		{
			using DataContext context = _context.CreateDbContext();

			var comment = await GetByIdAsync(commentId);
			if (comment == null) return;

			context.Comments.Remove(comment);

			_ = await context.SaveChangesAsync();
		}

		public async Task<CommentEntity> GetByIdAsync(Guid commentId)
		{
			using DataContext context = _context.CreateDbContext();

			var response = await context.Comments
				.Where(x => x.CommentId == commentId)
				.FirstOrDefaultAsync();

			return response;
		}

		public async Task UpdateAsync(CommentEntity comment)
		{
			using DataContext context = _context.CreateDbContext();

			var existsOnDb = await GetByIdAsync(comment.PostId);
			if (existsOnDb == null) return;

			context.Comments.Update(comment);

			_ = await context.SaveChangesAsync();
		}
	}
}
