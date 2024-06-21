namespace AdaSoftLibrary.Application.Common.Configurations;

/// <summary>
/// Konfigurácia aplikácie
/// </summary>
public class ApplicationOptions
{
    public const string SECTION_NAME = "Application";

    /// <summary>
    /// Uživateľské meno
    /// </summary>
    public string UserName { get; init; } = default!;

    /// <summary>
    /// Heslo
    /// </summary>
    public string Password { get; init; } = default!;

    /// <summary>
    /// Typ zdroja dát
    /// </summary>
    public DataSourceTypeEnum DataSourceType { get; init; } = default!;

    /// <summary>
    /// Cesta k XML súboru ("Library.xml")
    /// </summary>
    public string XmlFilePath { get; init; } = default!;
}
