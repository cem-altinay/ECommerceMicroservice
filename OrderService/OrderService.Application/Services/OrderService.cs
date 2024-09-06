using Ardalis.GuardClauses;
using ECommerce.Shared.Contracts;
using MassTransit;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository<Order> _orderRepository;
		private readonly IPublishEndpoint _publishEndpoint;
		public OrderService(IRepository<Order> orderRepository, IPublishEndpoint publishEndpoint)
		{
			_orderRepository = orderRepository;
			_publishEndpoint = publishEndpoint;
		}
		private void ValidateOrder(Order order)
		{
			/*
			  Bu aşamada api call yapıp çeşitli validasyonlar yapılabilir. Örneğin; Stok durumu kontrolü gibi.
			 */

			Guard.Against.Null(order, nameof(order), "Order cannot be null.");
			Guard.Against.NullOrEmpty(order.CustomerEmail, nameof(order.CustomerEmail), "Customer email is required.");
			Guard.Against.OutOfRange(order.OrderItems.Count, nameof(order.OrderItems), 1, int.MaxValue, "Order must contain at least one item.");

			// Her sipariş kalemi için validasyon
			foreach (var item in order.OrderItems)
			{
				Guard.Against.NullOrEmpty(item.ProductName, nameof(item.ProductName), "Product name is required.");
				Guard.Against.NegativeOrZero(item.Quantity, nameof(item.Quantity), "Product quantity must be greater than zero.");
				Guard.Against.NegativeOrZero(item.UnitPrice, nameof(item.UnitPrice), "Product unit price must be greater than zero.");
			}
		}
		public async Task CreateOrderAsync(Order order)
		{

			ValidateOrder(order);

			order.TotalPrice = order.OrderItems.Sum(item => item.UnitPrice);
			Console.WriteLine($"Order created for {order.OrderItems.Count} items, Total Price: {order.TotalPrice}");

			await _orderRepository.AddAsync(order);

			// Stok güncelleme mesajları oluştur
			foreach (var item in order.OrderItems)
			{
				var stockUpdateMessage = new StockUpdateMessageEvent
				{
					ProductId = item.Id,  
					Quantity = item.Quantity
				};

				await _publishEndpoint.Publish(stockUpdateMessage);
			}

	
			var notificationEmailEvent = new NotificationEvent
			{
				Recipient = order.CustomerEmail,
				Message = $"Your order with {order.OrderItems.Count} items has been placed. Total Price: {order.TotalPrice}",
				Type = NotificationType.Email
			};

			await _publishEndpoint.Publish(notificationEmailEvent);

			var notificationSmsEvent = new NotificationEvent
			{
				Recipient = order.CustomerEmail,
				Message = $"Your order with {order.OrderItems.Count} items has been placed. Total Price: {order.TotalPrice}",
				Type = NotificationType.Sms
			};

			await _publishEndpoint.Publish(notificationSmsEvent);
		}
	}

}
