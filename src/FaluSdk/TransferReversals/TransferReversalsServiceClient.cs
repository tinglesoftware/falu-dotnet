using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.TransferReversals;

///
public class TransferReversalsServiceClient : BaseServiceClient<TransferReversal>,
                                              ISupportsListing<TransferReversal, TransferReversalsListOptions>,
                                              ISupportsRetrieving<TransferReversal>,
                                              ISupportsCreation<TransferReversal, TransferReversalCreateRequest>,
                                              ISupportsUpdating<TransferReversal, TransferReversalPatchModel>
{
    ///
    public TransferReversalsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/transfer_reversals";

    /// <summary>List transfer reversals.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<TransferReversal>>> ListAsync(TransferReversalsListOptions? options = null,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List transfer reversals recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TransferReversal> ListRecursivelyAsync(TransferReversalsListOptions? options = null,
                                                                           RequestOptions? requestOptions = null,
                                                                           CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a transfer reversal.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer reversal</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> GetAsync(string id,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create transfer reversal.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> CreateAsync(TransferReversalCreateRequest request,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return CreateResourceAsync(request, options, cancellationToken);
    }

    /// <summary>
    /// Update a transfer reversal.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer reversal</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> UpdateAsync(string id,
                                                                        JsonPatchDocument<TransferReversalPatchModel> patch,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return UpdateResourceAsync(id, patch, options, cancellationToken);
    }
}
