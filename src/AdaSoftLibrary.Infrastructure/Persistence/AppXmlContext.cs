using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;

namespace AdaSoftLibrary.AdaSoft.Infrastructure.Persistence;

/// <summary>
/// XML dátový kontext
/// </summary>
public class AppXmlContext : IAppDataContext
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

    public List<Book> BookList => Library.Books ?? new();

    public int SaveChanges()
    {
        using (var writer = new StreamWriter(XmlFilePath))
        {
            XmlSerializer.Serialize(writer, Library);
        }

        return 1;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("The implementation of asynchronous storage is only in the database");
    }
}
