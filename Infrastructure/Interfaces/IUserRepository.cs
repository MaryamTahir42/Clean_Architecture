using EspressoPatronum.Models.Entities;
using System.Collections.Generic;

namespace EspressoPatronum.Models.Interfaces
{
	public interface IUserRepository
	{
		bool CheckUser(string email, string password);
		IEnumerable<User> GetAll();
		User GetById(int id);
		void Add(User user);
	}
}
