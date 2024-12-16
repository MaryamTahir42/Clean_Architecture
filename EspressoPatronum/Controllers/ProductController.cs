using EspressoPatronum.Models.Entities;
using EspressoPatronum.Services;
using Microsoft.AspNetCore.Mvc;

namespace EspressoPatronum.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductService _productService;

		public ProductController(ProductService productService)
		{
			_productService = productService;
		}

		public IActionResult ShowFood()
		{
			var productsList = _productService.GetProductsByCategory("food");
			Console.WriteLine("Food category loaded");
			return View(productsList);
		}

		public IActionResult ShowDrink()
		{
			var productsList = _productService.GetProductsByCategory("drink");
			Console.WriteLine("Drink category loaded");
			return View(productsList);
		}

		public IActionResult ShowDetails(int id)
		{
			var product = _productService.GetProductById(id);
			if (product == null) return NotFound();

			return View(product);
		}

		public IActionResult ProceedToCheckOut()
		{
			return View();
		}
	}
}
