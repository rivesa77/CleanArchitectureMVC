// <copyright file="PersonViewModelExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Models.Extensions
{
    using System;
    using System.Linq;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal static class PersonViewModelExtension
    {
        public static bool HasChanges(this PersonViewModel sourceClass, PersonEntity currentPerson)
        {
            return !string.Equals(sourceClass.DNI, currentPerson.DNI, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Name, currentPerson.Name, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Phone, currentPerson.Phone, StringComparison.Ordinal)
                || !string.Equals(sourceClass.Email, currentPerson.Email, StringComparison.Ordinal);
        }

        public static bool HasValidPhone(this PersonViewModel sourceClass)
        {
            return sourceClass?.Phone?.Length == 9
                && sourceClass.Phone.All(char.IsDigit);
        }

        public static bool HasValidDni(this PersonViewModel sourceClass)
        {
            return sourceClass?.DNI?.Length == 9;
        }
    }
}