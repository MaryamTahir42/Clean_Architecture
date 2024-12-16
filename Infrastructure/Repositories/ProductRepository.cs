using EspressoPatronum.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EspressoPatronum.Models.Repositories
{
	public class ProductRepository : GenericRepository<Product>
	{
		public ProductRepository(IConfiguration configuration) : base(configuration)
		{
		}


		public new void Add(Product entity)
		{
			base.Add(entity); 
		}

		public new IEnumerable<Product> GetAll()
		{
			return base.GetAll(); 
		}

		public new Product GetById(int id)
		{
			return base.GetById(id); 
		}

		public IEnumerable<Product> GetByCategory(string category)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var query = $"SELECT * FROM Product WHERE Category = @Category;";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Category", category);

					var reader = command.ExecuteReader();
					var products = new List<Product>();
					while (reader.Read())
					{
						products.Add(MapReaderToObject(reader));
					}
					return products;
				}
			}
		}

		private Product MapReaderToObject(SqlDataReader reader)
		{
			return new Product
			{
				Id = (int)reader["Id"],
				pname = reader["Name"].ToString(),
				category = reader["Category"].ToString(),
				description = reader["Description"].ToString()
			};
		}
	}
}
