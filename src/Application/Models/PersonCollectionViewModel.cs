// <copyright file="PersonCollectionViewModel.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Models
{
    public class PersonCollectionViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; }
    }
}