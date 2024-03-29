﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SeedWork;
using Ordering.Infrastructure;

namespace App.Services.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OrderRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order Add(Order order)
        {
            return  _context.Orders.Add(order).Entity;
               
        }

        public async Task<Order> GetAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.OrderItems).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.OrderStatus).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.Address).LoadAsync();
            }

            return order;
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}
