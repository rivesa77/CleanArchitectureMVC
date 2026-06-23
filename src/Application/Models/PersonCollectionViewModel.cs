// <copyright file="PersonCollectionViewModel.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Models
{
    public class PersonCollectionViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; }
    }
}