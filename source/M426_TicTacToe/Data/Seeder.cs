﻿using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace M426_TicTacToe.Data
{
    public enum Roles 
    {
        Administrator
    }

    public class Seeder
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            context.Database.EnsureCreated();

            #region Add Roles to DB
            if (!context.Roles.Any(r => r.Name == Roles.Administrator.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Administrator.ToString() });
            }
            #endregion

            context.SaveChanges();

            #region Add local users to DB
            if (!context.Users.Any(u => u.UserName == "test@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "test@test.com", UserName = "test@test.com" };
                await userManager.CreateAsync(user, "123456");
            }

            if (!context.Users.Any(u => u.UserName == "admin@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "admin@test.com", UserName = "admin@test.com" };
                await userManager.CreateAsync(user, "123456");
            }
            #endregion

            context.SaveChanges();

            #region Add individual users to DB
            if (!context.Users.Any(u => u.UserName == "nick@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "nick@test.com", UserName = "nick@test.com" };
                await userManager.CreateAsync(user, "123456");
            }

            if (!context.Users.Any(u => u.UserName == "tim@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "tim@test.com", UserName = "tim@test.com" };
                await userManager.CreateAsync(user, "123456");
            }

            if (!context.Users.Any(u => u.UserName == "marcel@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "marcel@test.com", UserName = "marcel@test.com" };
                await userManager.CreateAsync(user, "123456");
            }

            if (!context.Users.Any(u => u.UserName == "andrin@test.com"))
            {
                IdentityUser user = new IdentityUser { Email = "andrin@test.com", UserName = "andrin@test.com" };
                await userManager.CreateAsync(user, "123456");
            }
            #endregion

            context.SaveChanges();

            #region Add roles to users
            IdentityUser adminUser = context.Users.SingleOrDefault(u => u.Email == "admin@test.com");
            IdentityUser testUser = context.Users.SingleOrDefault(u => u.Email == "test@test.com");

            if (!(await userManager.IsInRoleAsync(adminUser, Roles.Administrator.ToString())))
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Administrator.ToString());
            }
            #endregion

            context.SaveChanges();
        }
    }
}
