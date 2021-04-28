﻿using ApiKeyAuth.Attributes;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ObslugaWydarzenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public abstract class SharedController<T>  : ControllerBase where T: EntityHelper.Entity
    {
        private readonly IRepository<T> repository;

        protected ILogger Logger { get; }

        public SharedController(IRepository<T> repository, ILogger logger)
        {
            this.repository = repository;
            Logger = logger;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Logger.LogInformation("Entered method Get");
                return Ok(await repository.GetListAsync());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await repository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }
        [HttpPost]
        [KeyAuthorize(RoleType.Customer, RoleType.Employee)]
        public async Task<ActionResult<T>> Post(T item)
        {
            await repository.AddAsync(item);
            await repository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [KeyAuthorize(RoleType.Customer, RoleType.Employee)]
        public async Task<ActionResult> Put(int id, T item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(item);
            try
            {
                await repository.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.FindBy(a => a.Id == id).Any())
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [KeyAuthorize(RoleType.Customer, RoleType.Employee)]
        public async Task<ActionResult<T>> Delete(int id)
        {
            var item = await repository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            await repository.SaveChangesAsync();
            return Ok(item);
        }

    }
}
