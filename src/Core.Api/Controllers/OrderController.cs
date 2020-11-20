using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Service;
using Service.Comomns;

namespace Core.Api.Controllers
{
    [Authorize( Roles = RoleHelper.Adm + ","  + RoleHelper.Seller)]
    [ApiController]
    [Route("Orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<OrderDto>>> GetAll(int page, int take=20)
        {
            var identity = this.User.Identity;
            return await _orderService.GetAll(page, take);
        }
        [HttpGet("{id}")]    
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            return await _orderService.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateDto model)
        {
            var result = await _orderService.Create(model);
            return CreatedAtAction(
                       "GetById",
                       new {id = result.OrderId},
                        result
                );
        }

    }
}
