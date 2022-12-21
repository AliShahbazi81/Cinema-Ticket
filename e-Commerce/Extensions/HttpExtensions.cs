using System.Text.Json;
using e_Commerce.RequestHelpers;
using Microsoft.AspNetCore.Http;

namespace e_Commerce.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData, options));
        // Since we are adding a custom header, we have to inform the platform that we are using that
        // otherwise, we would not be able to access PAGINATION header from our client-side 
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}