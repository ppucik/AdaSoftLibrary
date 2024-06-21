using AdaSoftLibrary.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AdaSoftLibrary.Domain.Entities;

/// <summary>
/// Kniha
/// </summary>
[Table("Book")]
public class Book : BaseEntity<int>
{
    /// <summary>
    /// Autor (max. 250 znakov)
    /// </summary>
    [Required]
    [MaxLength(250)]
    [XmlElement("Author")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov (max. 1000 znakov)
    /// </summary>
    [Required]
    [MaxLength(1000)]
    [XmlElement("Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Rok
    /// </summary>
    [XmlElement("Year")]
    public int? Year { get; set; }

    /// <summary>
    /// Popis
    /// </summary>
    [XmlElement("Description")]
    public string? Description { get; set; }

    /// <summary>
    /// <inheritdoc cref="Borrowed"/>
    /// </summary>
    [XmlElement("Borrowed")]
    public Borrowed? Borrowed { get; set; }

    /// <summary>
    /// Je kniha zapožičaná ?
    /// </summary>
    [NotMapped]
    [XmlIgnore]
    public bool IsBorrowed => Borrowed?.FromDate.HasValue ?? false;
}
