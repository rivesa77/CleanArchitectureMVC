// <copyright file="ApplicationDbContext.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Infrastructure.
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MVCPrueba1.Entities;

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PersonEntity> Persons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PersonEntity>(entity =>
            {
                entity.HasKey(person => person.Id);
                entity.Property(person => person.DNI).HasMaxLength(9).IsRequired();
                entity.Property(person => person.UserId).IsRequired();

                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(person => person.UserId);
            });
        }
    }
}
