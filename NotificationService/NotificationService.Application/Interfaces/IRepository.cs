using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces
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
