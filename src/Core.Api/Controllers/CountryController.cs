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
    [Route("Countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        
        [HttpGet]
        public async Task<ActionResult<DataCollection<CountryDto>>> GetAllAsync(int page, int take=20)
        {
            return await _countryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            return await _countryService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CountryCreateDto model)
        {
           var result = await _countryService.Create(model);
            return CreatedAtAction(
                "GetById",
                 new {id =result.CountryId},
                 result
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CountryUpdateDto model)
        {
            await _countryService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _countryService.Remove(id);
            return NoContent();
        }
   
    
    }
}
