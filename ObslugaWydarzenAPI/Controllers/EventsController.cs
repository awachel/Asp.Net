using Domain.Models;
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

    public class EventsController : SharedController<Event>
    {
        public EventsController(IRepository<Event> repository) : base(repository)
        {

        }
    }
}
