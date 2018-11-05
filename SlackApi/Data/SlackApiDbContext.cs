using Microsoft.EntityFrameworkCore;
using SlackApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Data
{
    public class SlackApiDbContext: DbContext
    {
        public SlackApiDbContext(DbContextOptions<SlackApiDbContext> options): base(options)
        { }

        public SlackApiDbContext()
        { }

        public DbSet<User> Users { get; set; }
    }
}
