using Domainn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domainn.DAL
{
    public class Context : DbContext
    {
        public DbSet<imie> imies { get; set; }

        public Context()
        {
            sqlServer = ConfigurationHelper.Configuration.GetConnectionString("SQLServer");
        }

        private string sqlServer;
    }
}
