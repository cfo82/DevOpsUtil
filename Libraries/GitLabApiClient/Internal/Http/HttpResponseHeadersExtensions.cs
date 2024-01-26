namespace GitLabApiClient.Internal.Http;

using System;
using System.Linq;
using System.Net.Http.Headers;
using GitLabApiClient.Internal.Utilities;

internal static class HttpResponseHeadersExtensions
{
    public static T GetFirstHeaderValueOrDefault<T>(
        this HttpResponseHeaders headers,
        string headerKey)
    {
        var toReturn = default(T);

        if (!headers.TryGetValues(headerKey, out var headerValues))
        {
            return toReturn;
        }

        string valueString = headerValues.FirstOrDefault();
        return valueString.IsNullOrEmpty() ? toReturn : (T)Convert.ChangeType(valueString, typeof(T));
    }
}
