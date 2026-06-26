// <copyright file="PersonsE2ETests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.E2E.Tests.Persons
{
    using System.Net;
    using System.Text.RegularExpressions;
    using FluentAssertions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Ricardo.MVCPrueba1.Domain.Entities;
    using Ricardo.MVCPrueba1.Infrastructure.Data;

    [TestClass]
    [TestCategory("E2E.Persons")]
    public class PersonsE2ETests
    {
        private static readonly string UniqueId = Guid.NewGuid().ToString("N")[..8];

        private static readonly string SearchTerm = $"E2E{UniqueId}";

        private static readonly string ConnectionString =
            $"Server=(localdb)\\mssqllocaldb;Database=MVCPrueba1_E2E_{UniqueId};Trusted_Connection=True;MultipleActiveResultSets=true";

        private static readonly PersonEntity Person = new PersonEntity
        {
            Name = $"Pepe{SearchTerm}",
            DNI = "80000001A",
            Phone = "600300001",
            Email = $"pepe.{UniqueId}@example.com",
            UserId = string.Empty,
        };

        private static readonly PersonEntity AnotherPerson = new PersonEntity
        {
            Name = $"Antonio{SearchTerm}",
            DNI = "80000002B",
            Phone = "600300002",
            Email = $"antonio.{UniqueId}@example.com",
            UserId = string.Empty,
        };

        [TestMethod]
        public async Task Persons_WhenCreated_CanBeSearchedAndSortedByNameDescending()
        {
            await using WebApplicationFactory<Program> factory = CreateFactory(ConnectionString);

            HttpClient client = factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost"),
                HandleCookies = true,
            });

            try
            {
                await RegisterUser(client).ConfigureAwait(false);
                await CreatePerson(client, Person).ConfigureAwait(false);
                await CreatePerson(client, AnotherPerson).ConfigureAwait(false);

                string escapeDataString = Uri.EscapeDataString(SearchTerm);

                string personsPage = await GetString(
                    client,
                    $"/Persons?searchField=Name&searchTerm={escapeDataString}&pageSize=5&sortField=Name&sortDirection=Descending")
                    .ConfigureAwait(false);

                personsPage
                    .Should()
                    .Contain(AnotherPerson.Name);

                personsPage
                    .Should()
                    .Contain(Person.Name);

                personsPage.IndexOf(Person.Name, StringComparison.Ordinal)
                    .Should()
                    .BeLessThan(
                        personsPage.IndexOf(AnotherPerson.Name, StringComparison.Ordinal),
                        "the Pepe person should appear before the Antonio person when sorting by name descending");
            }
            finally
            {
                await DeleteDatabase(ConnectionString).ConfigureAwait(false);
            }
        }

        private static WebApplicationFactory<Program> CreateFactory(string connectionString)
        {
            return new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("ConnectionStrings:DefaultConnection", connectionString);
                    builder.UseEnvironment("Development");
                });
        }

        private static async Task RegisterUser(HttpClient client)
        {
            string registerPageUrl = "/Identity/Account/Register";

            string registerPage = await GetString(client, registerPageUrl).ConfigureAwait(false);

            string token = GetAntiForgeryToken(registerPage);

            using HttpResponseMessage response = await client.PostAsync(
                registerPageUrl,
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    ["Input.Email"] = $"e2e.{UniqueId}@example.com",
                    ["Input.Password"] = "E2eTest1234!",
                    ["Input.ConfirmPassword"] = "E2eTest1234!",
                    ["__RequestVerificationToken"] = token,
                }))
                .ConfigureAwait(false);

            (response.StatusCode is HttpStatusCode.Redirect or HttpStatusCode.SeeOther)
                .Should()
                .BeTrue($"register should redirect after success, but received {(int)response.StatusCode}");
        }

        private static async Task CreatePerson(HttpClient client, PersonEntity personEntity)
        {
            using HttpResponseMessage response = await client.PostAsync(
                "/Persons/create",
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    ["DNI"] = personEntity.DNI,
                    ["Name"] = personEntity.Name,
                    ["Email"] = personEntity.Email,
                    ["Phone"] = personEntity.Phone,
                }))
                .ConfigureAwait(false);

            (response.StatusCode is HttpStatusCode.Redirect or HttpStatusCode.SeeOther)
                .Should()
                .BeTrue($"person create should redirect after success, but received {(int)response.StatusCode}");
        }

        private static async Task<string> GetString(HttpClient client, string path)
        {
            using HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);

            response.IsSuccessStatusCode
                .Should()
                .BeTrue($"GET {path} should succeed, but received {(int)response.StatusCode}");

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private static string GetAntiForgeryToken(string html)
        {
            Match match = Regex.Match(
                html,
                "name=\"__RequestVerificationToken\"[^>]*value=\"(?<token>[^\"]+)\"|value=\"(?<token>[^\"]+)\"[^>]*name=\"__RequestVerificationToken\"",
                RegexOptions.IgnoreCase);

            match.Success
                .Should()
                .BeTrue("the antiforgery token should be present in the register page");

            return WebUtility.HtmlDecode(match.Groups["token"].Value);
        }

        private static async Task DeleteDatabase(string connectionString)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            await using ApplicationDbContext context = new ApplicationDbContext(options);
            await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
        }
    }
}