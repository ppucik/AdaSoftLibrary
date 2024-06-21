namespace AdaSoftLibrary.Application.Common.Configurations;

/// <summary>
/// Typ zdroja dát
/// </summary>
public enum DataSourceTypeEnum
{
    /// <summary>
    /// XML súbor - potrebné nastaviť súčasne hodnotu <see cref="ApplicationOptions.XmlFilePath" /> (default)
    /// </summary>
    XML,

    /// <summary>
    /// SQLite databáza - potrebné je nastaviť tiež hodnotu "DefaultConnection" v konfigurácii
    /// </summary>
    SQLite
}
