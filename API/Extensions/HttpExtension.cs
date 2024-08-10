using API.RequestHelpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            var optins = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData, optins));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");


        }
    }
}
