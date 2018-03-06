using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HangoutsContext>
    {
        public HangoutsContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HangoutsContext>();

            builder.UseSqlServer(Configuration.ConnectionString);
            return new HangoutsContext(builder.Options);
        }
    }
}
