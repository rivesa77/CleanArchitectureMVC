// <copyright file="PersonViewModel.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PersonViewModel
    {
        public Guid Id { get; set; }

        public string DNI { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Person phone is required")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Person phone must contain exactly 9 numbers")]
        public string Phone { get; set; }

        public string ErrorMessage { get; set; }
    }
}
