﻿using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObslugaWydarzenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SharedController<T>  : ControllerBase where T: EntityHelper.Entity
    {
        private readonly IRepository<T> repository;

        public SharedController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
        public async Task<ActionResult<T>> Post(T item)
        {
            await repository.AddAsync(item);
            await repository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
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
