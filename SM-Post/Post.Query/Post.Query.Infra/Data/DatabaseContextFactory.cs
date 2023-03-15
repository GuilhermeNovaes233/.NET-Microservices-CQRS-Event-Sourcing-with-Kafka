using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infra.Data
{
	public class DatabaseContextFactory
	{
		private readonly Action<DbContextOptionsBuilder> _configureDbContext;

		public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
		{
			_configureDbContext = configureDbContext;
		}

		public DataContext CreateDbContext()
		{
			DbContextOptionsBuilder<DataContext> options = new();
			_configureDbContext(options);

			return new DataContext(options.Options);
		}
	}
}
