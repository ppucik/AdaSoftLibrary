using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AdaSoftLibrary.Domain.Entities;

/// <summary>
/// Údaje o výpožičke
/// </summary>
[Table("Borrowed")]
public class Borrowed
{
    private const string DATE_FORMAT = "d.M.yyyy";

    /// <summary>
    /// ID - len pre databázu
    /// </summary>
    [Key, ForeignKey("Book")]
    [XmlIgnore]
    public int BookId { get; set; } = default!;

    [XmlIgnore]
    public virtual Book Book { get; set; } = null!;

    /// <summary>
    /// Meno (max. 100 znakov)
    /// </summary>
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [XmlElement("FirstName")]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Priezvisko (max. 100 znakov)
    /// </summary>
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [XmlElement("LastName")]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Celé meno čitateľa
    /// </summary>
    [NotMapped]
    [XmlIgnore]
    public string FullName => $"{LastName} {FirstName}".Trim();

    /// <summary>
    /// Dátum zapožičania
    /// </summary>
    [DataType(DataType.Date)]
    [XmlIgnore]
    public DateOnly? FromDate { get; set; }

    /// <summary>
    /// Dátum zapožičania (format "d.M.yyyy")
    /// </summary>
    [NotMapped]
    [XmlElement("From")]
    public string From
    {
        get { return FromDate.HasValue ? FromDate.Value.ToString(DATE_FORMAT) : string.Empty; }
        set { FromDate = !string.IsNullOrEmpty(value) ? DateOnly.ParseExact(value, DATE_FORMAT) : null; }
    }
}
