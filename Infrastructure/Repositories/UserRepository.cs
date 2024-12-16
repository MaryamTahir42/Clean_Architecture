using EspressoPatronum.Models.Interfaces;
using EspressoPatronum.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EspressoPatronum.Models.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		private readonly IConfiguration _configuration;

		public UserRepository(IConfiguration configuration) : base(configuration)
		{
			_configuration = configuration;
		}

		public bool CheckUser(string email, string password)
		{
			const string query = "SELECT 1 FROM Users WHERE Email = @e AND Password = @P";
			var constring = _configuration.GetConnectionString("DefaultConnection");

			using (SqlConnection con = new SqlConnection(constring))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@e", email);
					cmd.Parameters.AddWithValue("@P", password);

					return cmd.ExecuteScalar() != null; // Returns true if any row is found
				}
			}
		}
	}
}
