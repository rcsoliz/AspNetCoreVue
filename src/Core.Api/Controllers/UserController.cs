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
   // [Authorize]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IClientService _clientService;
        public UserController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<ClientDto>>> GetAll(int page, int take=20)
        {
            return Ok();
            //return await _clientService.GetAllAsync(page, take);
        }

    }
}
