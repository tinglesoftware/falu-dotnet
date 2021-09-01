﻿using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Payments
{
    ///
    public class PaymentsService : BaseService<Payment>
    {
        ///
        public PaymentsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/payments";

        /// <summary>
        /// List payments.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<Payment>>> ListAsync(PaymentsListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Payment>> GetAsync(string id,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a payment.
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> CreateAsync(PaymentRequest payment,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            if (payment is null) throw new ArgumentNullException(nameof(payment));

            var uri = "/v1/payments";
            return await PostAsync<Payment>(uri, payment, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Payment>> UpdateAsync(string id,
                                                                   JsonPatchDocument<PaymentPatchModel> patch,
                                                                   RequestOptions? options = null,
                                                                   CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
