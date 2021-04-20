using Domainn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domainn.DAL
{
    public class Context : DbContext
    {
        public DbSet<Name> Names { get; set; }

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
