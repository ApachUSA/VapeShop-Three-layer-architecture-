using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Infrastructure.Interfaces;

namespace VapeShop.Infrastructure.Repositories
{
	public class Repository<T> : IBaseRepository<T> where T : class
	{
		private readonly VapeShopDbContext _dbContext;

		public Repository(VapeShopDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Create(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			 _dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public IQueryable<T> Get()
		{
			return _dbContext.Set<T>();
		}

		public async Task<T> Update(T entity)
		{
			_dbContext.Set<T>().Update(entity); 
			await _dbContext.SaveChangesAsync();

			return entity;
		}
	}
}
