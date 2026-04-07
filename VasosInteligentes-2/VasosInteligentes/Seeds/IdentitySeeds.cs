using Microsoft.AspNetCore.Identity;
using VasosInteligentes.Models;

namespace VasosInteligentes.Seeds
{
    public class IdentitySeeds
    {
        public static async Task SeedRolesAndUser(
            IServiceProvider serviceProvider,
            string defaultPassword)
        {
            // criação das roles (administrador e usuario)
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Models.ApplicationRole>>();
            string[] rolesNames = { "Administrador", "Usuario" };
            foreach (string roleName in rolesNames)
            {
                // verifica se ja foi criado
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    // se nao encontrar sera inserido
                    var result = await roleManager.CreateAsync(
                        new ApplicationRole { Name = roleName }
                    );
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"SEED: Role {roleName} foi criado");
                    }
                    else
                    {
                        return;
                    }
                }
            }
            // criar o administrador
            var userManager = serviceProvider.GetRequiredService<UserManager<Models.ApplicationUser>>();

            if (await userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                // se nao encontrar sera inserido
                var adminUser =
                    new ApplicationUser
                    {
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                    };

                var resultAdmin = await userManager.CreateAsync(adminUser, defaultPassword);

                if (resultAdmin.Succeeded)
                {
                    Console.WriteLine($"SEED: Administrador foi criado");
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
                else
                {
                    return;
                }


            }

            // criar um usuario comum
            if (await userManager.FindByEmailAsync("teste@usuario.com") == null)
            {
                // se nao encontrar sera inserido
                var user =
                    new ApplicationUser
                    {
                        UserName = "teste@usuario.com",
                        Email = "teste@usuario.com",
                        EmailConfirmed = true,
                    };

                var resultUser = await userManager.CreateAsync(user, "Teste@123");

                if (resultUser.Succeeded)
                {
                    Console.WriteLine($"SEED: Usuario comum foi criado");
                    await userManager.AddToRoleAsync(user, "Usuario");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
