﻿using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
	public interface IOrderService
	{
		Task CreateOrderAsync(Order order);
	}
}
