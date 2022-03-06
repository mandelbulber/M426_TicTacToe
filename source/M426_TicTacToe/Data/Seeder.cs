using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M426_TicTacToe.Data;
using Microsoft.AspNetCore.Identity;
using M426_TicTacToe.Models;

namespace M426_TicTacToe.Data
{
    public class Seeder
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
