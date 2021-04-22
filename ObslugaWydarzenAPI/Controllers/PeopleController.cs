using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObslugaWydarzenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository repository;

        public PeopleController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
