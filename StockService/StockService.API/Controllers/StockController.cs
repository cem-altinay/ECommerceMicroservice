using Microsoft.AspNetCore.Mvc;
using StockService.Application.Interfaces;

namespace StockService.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StockController : ControllerBase
	{

		private readonly ILogger<StockController> _logger;
		private readonly IStockService _stockService;

		public StockController(ILogger<StockController> logger, IStockService stockService)
		{
			_logger = logger;
			_stockService = stockService;
		}

		[HttpPost("Decrease")]
		public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockRequest request)
		{
			try
			{
				await _stockService.DecreaseStockAsync(request.ProductId, request.Quantity);
				return Ok(new { Message = "Stock updated successfully" });
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
			}
		}

		public class DecreaseStockRequest
		{
			public int ProductId { get; set; }
			public int Quantity { get; set; }
		}
	}
}
