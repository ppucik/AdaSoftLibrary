using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace AdaSoftLibrary.Domain.Entities;

/// <summary>
/// Knižnica
/// </summary>
public class Library
{
    [Required]
    [XmlElement("Book")]
    public List<Book> Books { get; set; } = new();
}
