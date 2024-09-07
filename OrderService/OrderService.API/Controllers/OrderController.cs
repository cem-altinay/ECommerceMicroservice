using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;

namespace OrderService.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{


		private readonly ILogger<OrderController> _logger;
		private readonly IOrderService _orderService;
		public OrderController(ILogger<OrderController> logger, IOrderService orderService)
		{
			_logger = logger;
			_orderService = orderService;
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto order)
		{
			try
			{
				await _orderService.CreateOrderAsync(order);
				return Ok(new { Message = "Order created successfully" });
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(new { Message = $"Missing or null argument: {ex.ParamName}", Details = ex.Message });
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return BadRequest(new { Message = $"Argument out of range: {ex.ParamName}", Details = ex.Message });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { Message = $"Invalid argument: {ex.ParamName}", Details = ex.Message });
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new { Message = "Operation failed", Details = ex.Message });
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { Message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
			}
		}
	}
}
