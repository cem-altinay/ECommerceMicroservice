using Microsoft.EntityFrameworkCore;
using StockService.Application.Interfaces;
using StockService.Domain.Entities;


namespace StockService.Infrastructure.Data
{
	public class GenericRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly StockContext _context;
		private DbSet<T> _entities;

		public GenericRepository(StockContext dbContext)
		{
			_context = dbContext;
			_entities = _context.Set<T>();
		}

		protected virtual DbSet<T> Entities => _entities ??= _context.Set<T>();

		public IQueryable<T> Table => Entities;

		public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task UpdateAsync(T entity)
		{		
		
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);			
			await _context.SaveChangesAsync();
		}

		public async Task<T?> GetByIdAsync(object id)
		{
			var entity = await Entities.FindAsync(id);

			return entity;
		}
	}
}
