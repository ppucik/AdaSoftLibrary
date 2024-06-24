﻿using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Filter pre zoznam kníh
/// </summary>
public enum BookFilterEnum
{
    /// <summary>
    /// Všetky knihy
    /// </summary>
    [Display(Name = "Všetky knihy")]
    AllBooks = 0,

    /// <summary>
    /// Voľné knihy
    /// </summary>
    [Display(Name = "Voľné knihy")]
    FreeBooks = 1,

    /// <summary>
    /// Požičané knihy
    /// </summary>
    [Display(Name = "Požičané knihy")]
    BorrowedBooks = 2
}