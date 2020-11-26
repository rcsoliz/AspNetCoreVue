﻿using System;
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
    [Authorize]
    [ApiController]
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductDto>>> GetAll(int page, int take=20)
        {
            return await _productService.GetAllAsync(page, take);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            return await _productService.GetById(id);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateDto model)
        {
            var result = await _productService.Create(model);
            return CreatedAtAction(
                    "GetById",
                    new { id = result.ProductId},
                    result
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductUpdateDto model)
        {
            await _productService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _productService.Remove(id);
            return NoContent();
        }

    }
}
