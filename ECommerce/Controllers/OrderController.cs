﻿using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly string connectionString;
        private readonly OrderService orderService;

        public OrderController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.orderService = new OrderService(
                new OrderRepository(connectionString),
                new CartRepository(connectionString), 
                new CostumerRepository(connectionString));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var products = this.orderService.Get();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody]Order order)
        {
            return Ok(this.orderService.Create(order.Cart, order.Customer));
        }
    }

}
