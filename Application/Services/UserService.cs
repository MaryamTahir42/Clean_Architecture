using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspressoPatronum.Models.Entities;
using EspressoPatronum.Models.Interfaces;

namespace EspressoPatronum.Services
{
	public class UserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAll();
		}

		public User GetUserById(int id)
		{
			return _userRepository.GetById(id);
		}

		public bool AuthenticateUser(string email, string password)
		{
			return _userRepository.CheckUser(email, password);
		}

		public User PrepareSignupUser(User user)
		{
			// Example: Add additional logic here if needed before creating a new user.
			return new User
			{
				Username = user.Username,
				Password = user.Password,
				Email = user.Email,
				Address = user.Address,
				Role = user.Role
			};
		}
		public void AddUser(User user)
		{
			_userRepository.Add(user); // Add user using the repository
		}
	}
}

