// <copyright file="PersonSearchQuery.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Repositories
{
    using Ricardo.MVCPrueba1.Application.Models;

    public class PersonSearchQuery
    {
        public string UserId { get; set; }

        public PersonSearchField SearchField { get; set; } = PersonSearchField.All;

        public string SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}
