namespace AdaSoftLibrary.Web.Common;

/// <summary>
/// Filter pre zoznam kníh
/// </summary>
public enum BookFilterEnum
{
    /// <summary>
    /// Všetky knihy
    /// </summary>
    AllBooks = 0,

    /// <summary>
    /// Voľné knihy
    /// </summary>
    FreeBooks = 1,

    /// <summary>
    /// Požičané knihy
    /// </summary>
    BorrowedBooks = 2
}
