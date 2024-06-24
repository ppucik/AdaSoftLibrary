using System.Globalization;
using System.Text;

namespace AdaSoftLibrary.Application.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// A string extension method that removes the diacritics character from the strings <see href="https://csharp-extension.com"/>.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The string without diacritics character.</returns>
    public static string RemoveDiacritics(this string @this)
    {
        string normalizedString = @this.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new();

        foreach (char t in normalizedString)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
