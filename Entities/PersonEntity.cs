// <copyright file="PersonEntity.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    public class PersonEntity
    {
        [Key]
        [MaxLength(9)]
        public Guid Id { get; set; }

        public required string DNI { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual IdentityUser User { get; set; }

        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
    }
}