﻿using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Falu.Infrastructure
{
    /// <summary>
    /// Model of a HTTP response to an API with typed Error and Resource
    /// </summary>
    /// <typeparam name="TResource">the type of resource</typeparam>
    /// <remarks>
    /// There is no need to implement <see cref="System.IDisposable"/> because there are no unmanaged resources in use
    /// and there are no resources that the Garbage Collector does not know how to release.
    /// The instance of <see cref="HttpResponseMessage"/> referenced by <see cref="Response"/> is automatically disposed
    /// once an instance of <see cref="ResourceResponse{TResource}"/> is no longer in use.
    /// </remarks>
    public class ResourceResponse<TResource>
    {
        /// <summary>
        /// Create an instance of <see cref="ResourceResponse{TResource}"/>
        /// </summary>
        /// <param name="response"></param>
        /// <param name="resource"></param>
        /// <param name="error"></param>
        public ResourceResponse(HttpResponseMessage response,
                                TResource resource = default,
                                FaluError error = default)
        {
            Response = response;
            Resource = resource;
            Error = error;

            RequestId = GetHeader(response.Headers, "X-Request-Id");
            IdempotencyKey = GetHeader(response.Headers, "Idempotency-Key");
            ContinuationToken = GetHeader(response.Headers, "X-Continuation");
        }

        /// <summary>Gets the ID of the request, as returned by Falu.</summary>
        public string RequestId { get; }

        /// <summary>Gets the idempotency key of the request, as returned by Falu.</summary>
        public string IdempotencyKey { get; }

        /// <summary>Gets the token to use to fetch more data, as returned by Falu.</summary>
        public string ContinuationToken { get; }

        /// <summary>
        /// The original HTTP response
        /// </summary>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// The response status code gotten from <see cref="Response"/>
        /// </summary>
        public HttpStatusCode StatusCode => Response.StatusCode;

        /// <summary>
        /// Determines if the request was successful. Value is true if the response code is in the 200 to 299 range
        /// </summary>
        public bool IsSuccessful => (int)Response.StatusCode >= 200 && (int)Response.StatusCode <= 299;

        /// <summary>
        /// The resource extracted from the response body
        /// </summary>
        public TResource Resource { get; set; }

        /// <summary>
        /// The error extracted from the response body
        /// </summary>
        public FaluError Error { get; set; }

        /// <summary>
        /// Helper method to ensure the response was successful
        /// </summary>
        public void EnsureSuccess()
        {
            // do not bother with successful requests
            if (IsSuccessful) return;

            throw new FaluException(statusCode: StatusCode, error: Error, message: Error.Detail ?? Error.Title)
            {
                Response = Response,
            };
        }

        /// <summary>
        /// Checks if there are more results to retrieve.
        /// The result is null when <typeparamref name="TResource"/> is not assignable from <see cref="IEnumerable"/>.
        /// Otherwise, true when <see cref="ContinuationToken"/> has a valueor false when it doesnt have a value.
        /// </summary>
        public bool? HasMoreResults => typeof(IEnumerable).IsAssignableFrom(typeof(TResource)) ? ContinuationToken != null : (bool?)null;

        private static string GetHeader(HttpResponseHeaders headers, string name)
        {
            if (headers.TryGetValues(name, out var values))
            {
                return values.SingleOrDefault();
            }

            return default;
        }
    }
}