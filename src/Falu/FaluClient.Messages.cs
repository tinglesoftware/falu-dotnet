﻿using Falu.Infrastructure;
using Falu.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu
{
    public partial class FaluClient
    {
        /// <summary>
        /// List messages.
        /// </summary>
        /// <param name="from">Starting date for the messages</param>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<List<Message>>> ListMessagesAsync(DateTimeOffset? from = null,
                                                                             int? count = null,
                                                                             string continuationToken = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            if (from != null) args["from"] = $"{from:o}";
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/messages{query}");
            return await GetAsJsonAsync<List<Message>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<Message>> GetMessageAsync(string id, CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await GetAsJsonAsync<Message>(uri, cancellationToken);
        }

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<Message>> SendMessageAsync(MessageCreateRequest message,
                                                                      CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages");
            return await PostAsJsonAsync<Message>(uri, message, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<Message>> UpdateMessageAsync(string id,
                                                                        JsonPatchDocument<MessagePatchModel> patch,
                                                                        CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await PatchAsJsonAsync<Message>(uri, patch, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Send a batch of messages.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<List<Message>>> SendMessagesBatchAsync(IEnumerable<MessageCreateRequest> messages,
                                                                                  CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages/bulk");
            return await PostAsJsonAsync<List<Message>>(uri, messages, cancellationToken: cancellationToken);
        }
    }
}
