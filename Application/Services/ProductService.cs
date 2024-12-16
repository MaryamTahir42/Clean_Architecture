using EspressoPatronum.Models.Entities;
using EspressoPatronum.Models.Repositories;
using System.Collections.Generic;

namespace EspressoPatronum.Services
{
	public class ProductService
	{
		private readonly ProductRepository _productRepository;

		public ProductService(ProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _productRepository.GetAll();
		}

		public Product GetProductById(int id)
		{
			return _productRepository.GetById(id);
		}

		public IEnumerable<Product> GetProductsByCategory(string category)
		{
			return _productRepository.GetByCategory(category);
		}

		public void AddProduct(Product product)
		{
			_productRepository.Add(product);
		}
	}
}
