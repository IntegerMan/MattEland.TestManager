using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MattEland.TestManager.Models;

namespace MattEland.TestManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MattEland.TestManager.Models.TestSuite> TestSuite { get; set; }
        public DbSet<MattEland.TestManager.Models.TestCase> TestCase { get; set; }
    }
}
