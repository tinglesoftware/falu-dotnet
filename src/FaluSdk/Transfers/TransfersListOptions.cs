using Falu.Core;

namespace Falu.Transfers;

/// <summary>Options for filtering and pagination of transfers.</summary>
public record TransfersListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="Transfer.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status);
    }
}
