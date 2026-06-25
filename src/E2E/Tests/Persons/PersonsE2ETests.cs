// <copyright file="PersonsE2ETests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.E2E.Tests.Persons
{
    using System.Net;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Ricardo.MVCPrueba1.Infrastructure.Data;

    [TestClass]
    [TestCategory("E2E.Persons")]
    public class PersonsE2ETests
    {
        [TestMethod]
        public async Task Persons_WhenCreated_CanBeSearchedAndSortedByNameDescending()
        {
            string uniqueId = Guid.NewGuid().ToString("N")[..8];
            string connectionString = CreateConnectionString(uniqueId);
            string searchTerm = $"E2E {uniqueId}";
            string aliceName = $"{searchTerm} Alice";
            string zedName = $"{searchTerm} Zed";

            await using WebApplicationFactory<Program> factory = CreateFactory(connectionString);
            HttpClient client = factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost"),
                HandleCookies = true,
            });

            try
            {
                await RegisterUser(client, uniqueId).ConfigureAwait(false);
                await CreatePerson(client, "80000001A", aliceName, $"alice.{uniqueId}@example.com", "600300001").ConfigureAwait(false);
                await CreatePerson(client, "80000002B", zedName, $"zed.{uniqueId}@example.com", "600300002").ConfigureAwait(false);

                string personsPage = await GetString(
                    client,
                    $"/Persons?searchField=Name&searchTerm={Uri.EscapeDataString(searchTerm)}&pageSize=5&sortField=Name&sortDirection=Descending")
                    .ConfigureAwait(false);

                StringAssert.Contains(personsPage, zedName);
                StringAssert.Contains(personsPage, aliceName);

                Assert.IsTrue(
                    personsPage.IndexOf(zedName, StringComparison.Ordinal) < personsPage.IndexOf(aliceName, StringComparison.Ordinal),
                    "Expected the Zed person to appear before the Alice person when sorting by name descending.");
            }
            finally
            {
                await DeleteDatabase(connectionString).ConfigureAwait(false);
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

        private static async Task RegisterUser(HttpClient client, string uniqueId)
        {
            string registerPage = await GetString(client, "/Identity/Account/Register").ConfigureAwait(false);
            string token = GetAntiForgeryToken(registerPage);

            using HttpResponseMessage response = await client.PostAsync(
                "/Identity/Account/Register",
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    ["Input.Email"] = $"e2e.{uniqueId}@example.com",
                    ["Input.Password"] = "E2eTest1234!",
                    ["Input.ConfirmPassword"] = "E2eTest1234!",
                    ["__RequestVerificationToken"] = token,
                }))
                .ConfigureAwait(false);

            Assert.IsTrue(
                response.StatusCode is HttpStatusCode.Redirect or HttpStatusCode.SeeOther,
                $"Expected register to redirect after success, but received {(int)response.StatusCode}.");
        }

        private static async Task CreatePerson(HttpClient client, string dni, string name, string email, string phone)
        {
            using HttpResponseMessage response = await client.PostAsync(
                "/Persons/create",
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    ["DNI"] = dni,
                    ["Name"] = name,
                    ["Email"] = email,
                    ["Phone"] = phone,
                }))
                .ConfigureAwait(false);

            Assert.IsTrue(
                response.StatusCode is HttpStatusCode.Redirect or HttpStatusCode.SeeOther,
                $"Expected person create to redirect after success, but received {(int)response.StatusCode}.");
        }

        private static async Task<string> GetString(HttpClient client, string path)
        {
            using HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);

            Assert.IsTrue(
                response.IsSuccessStatusCode,
                $"Expected GET {path} to succeed, but received {(int)response.StatusCode}.");

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private static string GetAntiForgeryToken(string html)
        {
            Match match = Regex.Match(
                html,
                "name=\"__RequestVerificationToken\"[^>]*value=\"(?<token>[^\"]+)\"|value=\"(?<token>[^\"]+)\"[^>]*name=\"__RequestVerificationToken\"",
                RegexOptions.IgnoreCase);

            Assert.IsTrue(match.Success, "The antiforgery token was not found in the register page.");

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

        private static string CreateConnectionString(string uniqueId)
        {
            return $"Server=(localdb)\\mssqllocaldb;Database=MVCPrueba1_E2E_{uniqueId};Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}
