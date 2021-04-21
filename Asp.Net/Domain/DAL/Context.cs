using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DAL
{
    public class Context : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<person> People { get; set; }

        public Context()
        {
            sqlServer = ConfigurationHelper.Configuration.GetConnectionString("SQLServer");
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }



        private readonly string sqlServer;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (sqlServer != null)
            {
                optionsBuilder.UseSqlServer(sqlServer);
            }
        }

    }
}
