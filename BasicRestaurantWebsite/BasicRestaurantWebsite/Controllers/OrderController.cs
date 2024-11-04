using BasicRestaurantWebsite.Data;
using BasicRestaurantWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasicRestaurantWebsite.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _context;
		private Repository<Product> _products;
		private Repository<Order> _orders;
		private readonly UserManager<ApplicationUser> _userManager;

		public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
			_products = new Repository<Product>(context);
			_orders = new Repository<Order>(context);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
			{
				OrderItems = new List<OrderItemViewModel>(),
				Products = await _products.GetAllAsync()
			};

			return View(model);
		}

		/*[Authorize]
		[HttpPost]
		public async Task<IActionResult>AddItem(int prodId, int prodQty)
		{
			var product = await _context.Procducts.FindAsync(prodId);
			if (product == null)
			{ 
			}
		}

		public IActionResult Index()
		{
			return View();
		}
	*/
	}
}
