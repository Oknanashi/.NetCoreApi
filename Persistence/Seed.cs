using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        // , RoleManager<IdentityRole> roleManager
        public static async Task SeedData(DataContext context, UserManager<AppEmployee> userManager)
        { 
            
            //fix roles
            // string[] roleNames = { "Admin", "Manager", "Member" };
            // IdentityResult roleResult;
            // bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            // if (!adminRoleExists)
            // {
            //     foreach (var roleName in roleNames)
            //     {

            //         roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

            //     }
            // }



            if (!userManager.Users.Any())
            {
                var employees = new List<AppEmployee>{
                    new AppEmployee{

                        DisplayName="kirill",
                        UserName="kirill",
                        Email="bob@tes.com",
                        Role = "Admin"

                    }
                };
                foreach (var employee in employees)
                {
                    var powerUser = await userManager.CreateAsync(employee, "Pa$$w0rd");
                    var userResult = await userManager.AddToRoleAsync(employee, "Admin");
                    
                }
            }
            

            






            await context.SaveChangesAsync();
        }
    }
}
