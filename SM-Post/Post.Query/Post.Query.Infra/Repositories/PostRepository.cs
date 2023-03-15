using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infra.Data;

namespace Post.Query.Infra.Repositories
{
	public class PostRepository : IPostRepository
	{
		private readonly DatabaseContextFactory _context;
		public PostRepository(DatabaseContextFactory context)
		{
			_context = context;
		}

		public async Task CreateAsync(PostEntity post)
		{
			using DataContext context = _context.CreateDbContext();

			context.Posts.Add(post);
			_ = await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid postId)
		{
			using DataContext context = _context.CreateDbContext();

			var post = await GetByIdAsync(postId);
			if (post == null) return;

			context.Posts.Remove(post);

			_ = await context.SaveChangesAsync();
		}

		public async Task<List<PostEntity>> GetAllAsync()
		{
			using DataContext context = _context.CreateDbContext();

			return await context.Posts
						.AsNoTracking()
						.Include(p => p.Comments)
						.ToListAsync();
		}

		public async Task<List<PostEntity>> GetByAuthorAsync(string author)
		{
			using DataContext context = _context.CreateDbContext();

			return await context.Posts
						.AsNoTracking()
						.Include(p => p.Comments).AsNoTracking()
						.Where(p => p.Author.Contains(author))
						.ToListAsync();
		}

		public async Task<PostEntity> GetByIdAsync(Guid postId)
		{
			using DataContext context = _context.CreateDbContext();

			var response = await context.Posts
				.Include(p => p.Comments)
				.Where(x => x.PostId == postId)
				.FirstOrDefaultAsync();

			return response;
		}

		public async Task<List<PostEntity>> GetWithCommentsAsync()
		{
			using DataContext context = _context.CreateDbContext();

			return await context.Posts
						.AsNoTracking()
						.Include(p => p.Comments).AsNoTracking()
						.Where(c => c.Comments != null && c.Comments.Any())
						.ToListAsync();
		}

		public async Task<List<PostEntity>> GetWithLikesAsync(int numberOfLikes)
		{
			using DataContext context = _context.CreateDbContext();

			return await context.Posts
						.AsNoTracking()
						.Include(p => p.Comments).AsNoTracking()
						.Where(c => c.Likes >= numberOfLikes)
						.ToListAsync();
		}

		public async Task UpdateAsync(PostEntity post)
		{
			using DataContext context = _context.CreateDbContext();

			var existsOnDb = await GetByIdAsync(post.PostId);
			if (existsOnDb == null) return;

			context.Posts.Update(post);

			_ = await context.SaveChangesAsync();
		}
	}
}
