using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpPost("Send")]
		public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
		{
			var notification = new Notification
			{
				Recipient = request.Recipient,
				Message = request.Message,
				Type = request.Type,
				SentDate = DateTime.UtcNow
			};

			
			try
			{
				await _notificationService.SendNotificationAsync(notification);
				return Ok(new { Message = "Notification sent successfully" });
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
			}		
		}

	}

	public class NotificationRequest
	{
		public string Recipient { get; set; }
		public string Message { get; set; }
		public NotificationType Type { get; set; }
	}
}
