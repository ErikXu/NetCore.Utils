using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetCore.Utils.Http
{
    public static class HttpRequestExtensions
    {
        public static Dictionary<string, string> GetHeaders(this HttpRequest request, string key)
        {
            return request.Headers.ToDictionary(kv => kv.Key, kv => kv.Value.First());
        }

        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.TryGetValue(key, out var values) ? values.First() : null;
        }

        public static Dictionary<string, string> GetCookies(this HttpRequest request, string key)
        {
            return request.Cookies.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static string GetCookie(this HttpRequest request, string key)
        {
            return request.Cookies.TryGetValue(key, out var value) ? value : null;
        }

        public static Dictionary<string, string> GetQueries(this HttpRequest request)
        {
            return request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.First());
        }

        public static string GetQuery(this HttpRequest request, string key)
        {
            return request.Query.TryGetValue(key, out var values) ? values.First() : null;
        }
    }
}