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

    [Authorize(Roles = RoleHelper.Adm)]
    [ApiController]
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<CategoryDto>>> GetAll(int page, int take = 20)
        {
            return await _categoryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            return await _categoryService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateDto model)
        {
            var result = await _categoryService.Create(model);
            return CreatedAtAction(
                     "GetById",
                     new { id = result.CategoryId},
                     result
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryUpdateDto model)
        {
            await _categoryService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove (int id)
        {
            await _categoryService.Remove(id);
            return NoContent();
        }
            
    }
}
