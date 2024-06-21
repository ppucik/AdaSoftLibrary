using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;

namespace AdaSoftLibrary.AdaSoft.Infrastructure.Persistence;

/// <summary>
/// XML dátový kontext
/// </summary>
public class AppXmlContext
{
    private readonly ApplicationOptions _options;

    public AppXmlContext(IOptions<ApplicationOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        if (!File.Exists(XmlFilePath))
            throw new ArgumentException($"Dátový XML súbor {XmlFilePath} neexistuje");

        using (var reader = new StreamReader(_options.XmlFilePath))
        {
            if (reader != null)
            {
                Library = XmlSerializer.Deserialize(reader) as Library ?? new();
            }
        }
    }

    private XmlSerializer XmlSerializer => new XmlSerializer(typeof(Library));

    public string XmlFilePath => _options.XmlFilePath;

    public Library Library { get; init; } = new();

    public List<Book> Books => Library.Books ?? new();

    /// <summary>
    /// Uloženie zmien do súboru
    /// </summary>
    public void SaveChanges()
    {
        using (var writer = new StreamWriter(XmlFilePath))
        {
            XmlSerializer.Serialize(writer, Library);
        }
    }
}
