// <copyright file="PersonEntity.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Domain.Entities
{
    public class PersonEntity
    {
        public Guid Id { get; set; }

        public required string DNI { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public required string UserId { get; set; }
    }
}
