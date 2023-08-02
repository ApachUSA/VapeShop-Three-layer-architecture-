using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Infrastructure.Interfaces
{
	public interface IBaseRespository<T> where T : class
	{
		Task Create(T entity);

		Task Delete(T entity);

		IQueryable<T> Get();

		Task<T> Update(T entity);
	}
}
