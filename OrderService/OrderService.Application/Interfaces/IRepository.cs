using OrderService.Domain.Entities;


namespace OrderService.Application.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> Table { get; }
		IQueryable<T> TableNoTracking { get; }

		Task<T> AddAsync(T entity);
		Task DeleteAsync(T entity);
		Task<T?> GetByIdAsync(object id);
		Task UpdateAsync(T entity);
	}
}
