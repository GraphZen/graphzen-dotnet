using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class GraphQLRequestExtensions
    {
        public static HttpRequestMessage ToHttpRequest(this GraphQLRequest request)
        {
            var requestJson = JsonSerializer.Serialize(request);
            var requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = requestJsonContent
            };
            return message;
        }
    }
}