using EspressoPatronum.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace EspressoPatronum.Models.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		protected readonly string _connectionString;

		public GenericRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public void Add(TEntity entity)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var tableName = typeof(TEntity).Name + "s";
				var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

				var columnNames = string.Join(",", properties.Select(p => p.Name));
				var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

				var query = $"INSERT INTO [{tableName}] ({columnNames}) VALUES ({parameterNames});";

				using (var command = new SqlCommand(query, connection))
				{
					foreach (var property in properties)
					{
						command.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity) ?? DBNull.Value);
					}
					command.ExecuteNonQuery();
				}
			}
		}

		public TEntity GetById(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var tableName = typeof(TEntity).Name + "s";
				var query = $"SELECT * FROM [{tableName}] WHERE Id = @Id;";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);

					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return MapReaderToObject(reader);
						}
						return null;
					}
				}
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var tableName = typeof(TEntity).Name + "s";
				var query = $"SELECT * FROM [{tableName}];";

				using (var command = new SqlCommand(query, connection))
				using (var reader = command.ExecuteReader())
				{
					var entities = new List<TEntity>();
					while (reader.Read())
					{
						entities.Add(MapReaderToObject(reader));
					}
					return entities;
				}
			}
		}

		public void Delete(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var tableName = typeof(TEntity).Name + "s";
				var query = $"DELETE FROM [{tableName}] WHERE Id = @Id;";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.ExecuteNonQuery();
				}
			}
		}

		private TEntity MapReaderToObject(SqlDataReader reader)
		{
			var entity = Activator.CreateInstance<TEntity>();

			foreach (var property in typeof(TEntity).GetProperties())
			{
				try
				{
					if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
					{
						property.SetValue(entity, reader[property.Name]);
					}
				}
				catch
				{
					// Safely ignore properties that don't exist in the result set
				}
			}
			return entity;
		}
	}
}
