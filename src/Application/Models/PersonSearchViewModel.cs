// <copyright file="PersonSearchViewModel.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Models
{
    public class PersonSearchViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; } = [];

        public PersonSearchField SearchField { get; set; } = PersonSearchField.All;

        public string SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5;

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.TotalPages > 0 && this.PageNumber < this.TotalPages;
    }
}
