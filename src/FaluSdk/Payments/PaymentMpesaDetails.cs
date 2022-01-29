﻿using System.Text.Json.Serialization;

namespace Falu.Payments;

/// <summary>
/// Details about an MPESA Payment
/// </summary>
public class PaymentMpesaDetails
{
    /// <summary>
    /// The target business short code
    /// </summary>
    [JsonPropertyName("business_short_code")]
    public string? BusinessShortCode { get; set; }

    /// <summary>
    /// Type of payment.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Reference the payment was made in.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Phone number that made the payment, in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Unique identifier for request as issued by MPESA.
    /// Only populated for flows that initiate the transaction instead of MPESA.
    /// The value is only available after the request is sent to MPESA.
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Unique transaction identifier generated by MPESA.
    /// Only populated for completed transactions.
    /// </summary>
    public string? Receipt { get; set; }

    /// <summary>
    /// Name of the entity making or that made the payment.
    /// </summary>
    public string? Payer { get; set; }
}
