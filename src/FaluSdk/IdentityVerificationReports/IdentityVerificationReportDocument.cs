using Falu.Core;

namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportDocument : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Expiry date of the document.
    /// </summary>
    public DateTimeOffset? Expiry { get; set; }

    /// <summary>
    /// Issued date of the document.
    /// </summary>
    public DateTimeOffset? Issued { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, which issued the document.
    /// </summary>
    /// <example>ken</example>
    public string? Issuer { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, where the entity issued the document originates from.
    /// </summary>
    /// <example>ken</example>
    public string? Nationality { get; set; }

    /// <summary>
    /// Type of the document.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Sub type for the document
    /// </summary>
    public string? SubType { get; set; }

    /// <summary>
    /// Document identification number.
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Personal number
    /// </summary>
    public string? PersonalNumber { get; set; }

    /// <summary>
    /// First name as it appears in the document.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name as it appears in the document.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Other names as they appear in the document.
    /// </summary>
    public List<string>? OtherNames { get; set; }

    /// <summary>
    /// Sex as it appears on the document
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// Date of birth as it appears in the document.
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// Address as it appears in the document.
    /// </summary>
    public PhysicalAddress? Address { get; set; }

    /// <summary>
    /// Unique identifiers of the files containing images for this document.
    /// </summary>
    public List<string> Files { get; set; } = new List<string>();
}
