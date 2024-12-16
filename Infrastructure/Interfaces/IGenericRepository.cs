using System.Collections.Generic;

namespace EspressoPatronum.Models.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);         
		TEntity GetById(int id);        
		IEnumerable<TEntity> GetAll();     
		void Delete(int id);               
	}
}
