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

		[Authorize]
		[HttpPost]
		public async Task<IActionResult>AddItem(int prodId, int prodQty)
		{
			var product = await _context.Products.FindAsync(prodId);
			if (product == null)
			{
				return NotFound();
			}

			// Retreive or create an OrderViewModel from session or other state managements
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
			{
				OrderItems = new List<OrderItemViewModel>(),
				Products = await _products.GetAllAsync()
			};

			// Check if the product is already in the order
			var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

			// If product is in order update quantity
			if (existingItem != null)
			{
				existingItem.Quantity += prodQty;
			}
			else
			{
				model.OrderItems.Add(new OrderItemViewModel
				{
					ProductId = product.ProductId,
					Price = product.Price,
					Quantity = prodQty,
					ProductName = product.Name
				});
			}

			// Update the total amount
			model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

			//Save updated OrderViewModel to session
			HttpContext.Session.Set("OrderViewModel", model);

			return RedirectToAction("Create", model);
		}

        [Authorize]
        [HttpGet]
		public async Task<IActionResult> Cart()
		{
			// Retrieve the OrderViewModel from session or other state management
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

			if (model == null || model.OrderItems.Count == 0)
			{
				return RedirectToAction("Create");
			}

			return View(model);
        }

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> PlaceOrder()
		{
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
			if (model == null || model.OrderItems.Count == 0)
			{
				return RedirectToAction("Create");
			}

			// Create a new Order entity
			Order order = new Order
			{
				OrderDate = DateTime.Now,
				TotalAmount = model.TotalAmount,
				UserID = _userManager.GetUserId(User)
			};

			// Add OrderItems to the Order entity
			foreach (var item in model.OrderItems) 
			{
				order.OrderItems.Add(new OrderItem 
				{ 
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					Price = item.Price
				});
			}

			// Save Order Entity to database
			await _orders.AddAsync(order);

			// Clear the OrderViewModel from session or other state management
			HttpContext.Session.Remove("OrderViewModel");

			return RedirectToAction("ViewOrders");
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> ViewOrders()
		{
			var userId = _userManager.GetUserId(User);

			var userOrders = await _orders.GetAllByIdAsync(userId, "UserID", new QueryOptions<Order>
			{
				Includes = "OrderItems.Product"
			});

			return View(userOrders);
		}
	}
}
