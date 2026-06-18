// <copyright file="PersonEntity.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class PersonEntity
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }
    }
}