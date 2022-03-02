using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using M426_TicTacToe.Models;

namespace M426_TicTacToe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<History> Histories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
