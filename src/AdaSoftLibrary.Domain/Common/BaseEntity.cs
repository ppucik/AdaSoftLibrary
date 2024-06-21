using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace AdaSoftLibrary.Domain.Common;

public abstract class BaseEntity<TKey> : IHasKey<TKey>
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    [XmlAttribute("id")]
    public TKey Id { get; set; } = default!;
}
