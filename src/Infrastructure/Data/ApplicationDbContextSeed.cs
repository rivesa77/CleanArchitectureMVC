// <copyright file="ApplicationDbContextSeed.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.MVCPrueba1.Domain.Entities;

    public static class ApplicationDbContextSeed
    {
        public const string DemoUserEmail = "demo@ricardo.local";

        public const string DemoUserPassword = "Demo1234!";

        private const string DniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (await context.Persons.AnyAsync().ConfigureAwait(false))
            {
                return;
            }

            IdentityUser demoUser = await GetOrCreateDemoUser(serviceProvider).ConfigureAwait(false);

            await context.Persons
                .AddRangeAsync(CreatePersons(demoUser.Id))
                .ConfigureAwait(false);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private static async Task<IdentityUser> GetOrCreateDemoUser(IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser demoUser = await userManager.FindByEmailAsync(DemoUserEmail).ConfigureAwait(false);

            if (demoUser is not null)
            {
                return demoUser;
            }

            demoUser = new IdentityUser()
            {
                UserName = DemoUserEmail,
                Email = DemoUserEmail,
                EmailConfirmed = true,
            };

            IdentityResult result = await userManager.CreateAsync(demoUser, DemoUserPassword).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Demo user seed failed: {errors}");
            }

            return demoUser;
        }

        private static IEnumerable<PersonEntity> CreatePersons(string userId)
        {
            string[] names =
            [
                "Alejandro Garcia Lopez",
                "Lucia Martinez Ruiz",
                "Carlos Sanchez Moreno",
                "Maria Fernandez Castro",
                "Javier Rodriguez Gil",
                "Laura Gomez Navarro",
                "Daniel Perez Ortega",
                "Sofia Diaz Romero",
                "Adrian Hernandez Vega",
                "Paula Alvarez Molina",
                "Miguel Torres Ramos",
                "Carmen Jimenez Santos",
                "Alberto Moreno Iglesias",
                "Elena Munoz Herrera",
                "Sergio Romero Medina",
                "Marta Navarro Leon",
                "David Ruiz Castillo",
                "Nuria Ortega Delgado",
                "Pablo Molina Prieto",
                "Raquel Vega Campos",
                "Ivan Castro Serrano",
                "Cristina Ramos Fuentes",
                "Oscar Gil Carrasco",
                "Beatriz Santos Arias",
                "Ruben Iglesias Caballero",
                "Patricia Herrera Nieto",
                "Hector Medina Pardo",
                "Monica Leon Cortes",
                "Victor Castillo Reyes",
                "Ana Delgado Pascual",
                "Hugo Prieto Gallego",
                "Silvia Campos Vidal",
                "Marcos Serrano Rojas",
                "Irene Fuentes Aguilar",
                "Jorge Carrasco Soler",
                "Claudia Arias Ferrer",
                "Raul Caballero Cano",
                "Teresa Nieto Lara",
                "Guillermo Pardo Andres",
                "Rocio Cortes Marin",
                "Samuel Reyes Calvo",
                "Natalia Pascual Rubio",
                "Andres Gallego Sanz",
                "Isabel Vidal Pastor",
                "Enrique Rojas Vicente",
                "Eva Aguilar Hidalgo",
                "Fernando Soler Mora",
                "Alicia Ferrer Lozano",
                "Mario Cano Blasco",
                "Sara Lara Esteban",
                "Jaime Andres Salas",
                "Olga Marin Ponce",
                "Diego Calvo Bravo",
                "Julia Rubio Crespo",
                "Tomas Sanz Pena",
                "Noelia Pastor Saez",
                "Gonzalo Vicente Rivas",
                "Miriam Hidalgo Soto",
                "Borja Mora Valero",
                "Emma Lozano Roldan",
                "Alfonso Benitez Robles",
                "Lidia Merino Lara",
                "Francisco Roman Castro",
                "Ainhoa Arias Navas",
                "Manuel Salazar Ortega",
                "Veronica Vila Campos",
                "Eduardo Mendez Gil",
                "Marina Cuesta Martin",
                "Alvaro Lozano Bravo",
                "Carolina Redondo Mora",
                "Ignacio Ponce Valero",
                "Belen Cordero Prieto",
                "Rafael Pastor Cano",
                "Ines Blasco Soria",
                "Julian Mora Serrano",
                "Angela Rey Moreno",
                "Cesar Calvo Ramos",
                "Lorena Nieto Soler",
                "Gabriel Ferrer Vidal",
                "Victoria Rivas Leon",
                "Rodrigo Paredes Molina",
                "Esther Hidalgo Fuentes",
                "Nicolas Soto Aguilar",
                "Mireia Esteban Romero",
                "Felipe Garrido Santos",
                "Diana Cabrera Ruiz",
                "Mateo Salas Vega",
                "Sandra Pascual Torres",
                "Ismael Vera Delgado",
                "Celia Robles Jimenez",
                "Arturo Merino Navarro",
                "Amalia Roman Herrera",
                "Ernesto Navas Medina",
                "Gloria Salazar Cortes",
                "Joel Vila Reyes",
                "Aitana Mendez Gallego",
                "Bruno Cuesta Rojas",
                "Lola Redondo Iglesias",
                "Gael Cordero Carrasco",
                "Nerea Paredes Lozano",
                "Cristobal Robles Cano",
                "Margarita Merino Arias",
                "Leandro Roman Lara",
                "Rosa Navas Serrano",
                "Emilio Salazar Soria",
                "Valeria Vila Blasco",
                "Dario Mendez Pardo",
                "Pilar Cuesta Cortes",
                "Esteban Redondo Gallego",
                "Alba Cordero Reyes",
                "Ramiro Paredes Pastor",
                "Sonia Benitez Mora",
                "Tania Robles Fuentes",
                "Marcel Merino Calvo",
                "Adela Roman Valero",
                "Joaquin Navas Carrasco",
                "Rebeca Salazar Hidalgo",
                "Cayetano Vila Roldan",
                "Nora Mendez Lozano",
                "Elias Cuesta Bravo",
                "Aurora Redondo Martin",
                "Martin Cordero Delgado",
                "Inmaculada Paredes Rivas",
                "Damian Benitez Nieto",
                "Susana Robles Pascual",
            ];

            return names.Select((name, index) => new PersonEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DNI = CreateDni(20000000 + index),
                Name = name,
                Email = CreateEmail(name),
                Phone = (600100000 + index).ToString(),
            });
        }

        private static string CreateDni(int number)
        {
            return $"{number}{DniLetters[number % DniLetters.Length]}";
        }

        private static string CreateEmail(string name)
        {
            return $"{name.ToLowerInvariant().Replace(" ", ".")}@example.com";
        }
    }
}
