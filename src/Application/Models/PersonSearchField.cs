// <copyright file="PersonSearchField.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Models
{
    public enum PersonSearchField
    {
        /// <summary>
        ///     Searches across every searchable person field.
        /// </summary>
        All,

        /// <summary>
        ///     Searches by DNI.
        /// </summary>
        Dni,

        /// <summary>
        ///     Searches by name.
        /// </summary>
        Name,

        /// <summary>
        ///     Searches by email.
        /// </summary>
        Email,

        /// <summary>
        ///     Searches by phone.
        /// </summary>
        Phone,
    }
}
