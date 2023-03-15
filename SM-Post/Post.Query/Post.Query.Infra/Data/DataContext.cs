using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;

namespace Post.Query.Infra.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options): base(options) 
		{
		}

		public DbSet<PostEntity> Posts { get; set; }
		public DbSet<CommentEntity> Comments { get; set; }
	}
}