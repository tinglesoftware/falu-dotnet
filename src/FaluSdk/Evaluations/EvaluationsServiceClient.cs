﻿using Falu.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Evaluations
{
    ///
    public class EvaluationsServiceClient : BaseServiceClient<Evaluation>, ISupportsListing<Evaluation, EvaluationsListOptions>
    {
        ///
        public EvaluationsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/evaluations";

        /// <summary>List evaluations.</summary>
        /// <inheritdoc/>
        public virtual Task<ResourceResponse<List<Evaluation>>> ListAsync(EvaluationsListOptions? options = null,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>List evaluations recursively.</summary>
        /// <inheritdoc/>
        public IAsyncEnumerable<Evaluation> ListRecursivelyAsync(EvaluationsListOptions? options = null,
                                                                 RequestOptions? requestOptions = null,
                                                                 CancellationToken cancellationToken = default)
        {
            return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> GetAsync(string id,
                                                                   RequestOptions? options = null,
                                                                   CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Update an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> UpdateAsync(string id,
                                                                      JsonPatchDocument<EvaluationPatchModel> patch,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Initiate an evaluation.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> CreateAsync(EvaluationCreateRequest evaluation,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            if (evaluation is null) throw new ArgumentNullException(nameof(evaluation));
            if (evaluation.Scope is null) throw new InvalidOperationException($"{nameof(evaluation.Scope)} cannot be null.");
            if (evaluation.Provider is null) throw new InvalidOperationException($"{nameof(evaluation.Provider)} cannot be null.");
            if (string.IsNullOrWhiteSpace(evaluation.Name))
            {
                throw new InvalidOperationException($"{nameof(evaluation.Name)} cannot be null or whitespace.");
            }

            var content = new MultipartFormDataContent
            {
                // populate fields of the model as key value pairs
                { new StringContent(evaluation.Currency), "currency" },
                { new StringContent(evaluation.Scope?.GetEnumMemberAttrValueOrDefault()), "scope" },
                { new StringContent(evaluation.Provider?.GetEnumMemberAttrValueOrDefault()), "provider" },
                { new StringContent(evaluation.Name), "name" },

                // populate the file stream
                { new StreamContent(evaluation.Content), "file", evaluation.FileName },
            };

            // Add phone if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Phone))
            {
                content.Add(new StringContent(evaluation.Phone), "phone");
            }

            // Add password if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Password))
            {
                content.Add(new StringContent(evaluation.Password), "password");
            }

            // Add description if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Description))
            {
                content.Add(new StringContent(evaluation.Description), "description");
            }

            // Add metadata if provided
            var metadata = evaluation.Metadata?.ToList();
            if (metadata != null)
            {
                for (var i = 0; i < metadata.Count; i++)
                {
                    content.Add(new StringContent(metadata[i].Key), $"metadata[{i}].Key");
                    content.Add(new StringContent(metadata[i].Value), $"metadata[{i}].Value");
                }
            }

            var uri = MakePath();
            return RequestAsync<Evaluation>(uri, HttpMethod.Post, content, options, cancellationToken);
        }


        /// <summary>
        /// Score an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> ScoreAsync(string id,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/score";
            return RequestAsync<Evaluation>(uri, HttpMethod.Post, new { }, options, cancellationToken);
        }
    }
}