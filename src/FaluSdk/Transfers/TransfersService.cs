﻿using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Transfers
{
    ///
    public class TransfersService : BaseService<Transfer>
    {
        ///
        public TransfersService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/transfers";

        /// <summary>
        /// List transfers.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Transfer>>> ListAsync(TransfersListOptions? options = null,
                                                                              RequestOptions? requestOptions = null,
                                                                              CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = $"/v1/transfers{query}";
            return await GetResourceAsync<List<Transfer>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Transfer>> GetAsync(string id,
                                                                 RequestOptions? options = null,
                                                                 CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a transfer.
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Transfer>> CreateAsync(TransferRequest transfer,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            if (transfer is null) throw new ArgumentNullException(nameof(transfer));

            var uri = "/v1/transfers";
            return await PostAsync<Transfer>(uri, transfer, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Transfer>> UpdateAsync(string id,
                                                                    JsonPatchDocument<TransferPatchModel> patch,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
