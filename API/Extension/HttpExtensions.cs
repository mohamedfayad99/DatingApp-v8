using System.Text.Json;
using API.Helpers;

namespace API.Extension;

public static class HttpExtensions
{
    public static void AddPaginnationHeader<T>(this HttpResponse response, PagedList<T> data){
        var paginationheader=new PagginationHeader(data.CurrentPage,data.PageSize,data.TotalPages,data.TotalCount);
        var jsonOptions=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
        response.Headers.Append("Pagination",JsonSerializer.Serialize(paginationheader,jsonOptions));
        response.Headers.Append("Access-control-Expose-Headers","Pagination");
    }

}
