// <copyright file="PersonViewModelExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Models.Extensions
{
    using System;
    using Ricardo.Application.Models;
    using Ricardo.Domain.Entities;

    internal static class PersonViewModelExtension
    {
        public static bool HasChanges(this PersonViewModel sourceClass, PersonEntity currentPerson)
        {
            return !string.Equals(sourceClass.DNI, currentPerson.DNI, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Name, currentPerson.Name, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Phone, currentPerson.Phone, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Email, currentPerson.Email, StringComparison.Ordinal);
        }
    }
}