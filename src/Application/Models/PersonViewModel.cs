// <copyright file="PersonViewModel.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Models
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }

        public string DNI { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ErrorMessage { get; set; }
    }
}