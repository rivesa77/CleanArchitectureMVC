// <copyright file="MemoryDbContext.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Infrastructure.Tests.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Ricardo.MVCPrueba1.Infrastructure.Data;

    internal class MemoryDbContext
    {
        public static ApplicationDbContext GetInMemoryDbContext()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptions<ApplicationDbContext> databaseOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new ApplicationDbContext(databaseOptions);
        }
    }
}