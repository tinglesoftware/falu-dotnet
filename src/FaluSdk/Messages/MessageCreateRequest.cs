namespace Falu.Messages;

/// <summary>
/// Information for creating and sending message.
/// </summary>
public class MessageCreateRequest : MessagePatchModel
{
    /// <summary>
    /// Destination phone numbers in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// You can provide up to 500 numbers in one request.
    /// </summary>
    public IList<string>? To { get; set; }

    /// <summary>
    /// Actual message content to be sent.
    /// Required if <see cref="Template"/> is not specified.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The template to use.
    /// Required if <see cref="Body"/> is not specified.
    /// </summary>
    public MessageSourceTemplate? Template { get; set; }

    /// <summary>
    /// The stream to use.
    /// It can either be the name or unique identifier of the stream.
    /// If not provided, message will default to the "transactional" stream.
    /// </summary>
    public string Stream { get; set; } = "transactional";
}
