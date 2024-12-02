using APICatalogo.Domain.Responses;
using Microsoft.AspNetCore.Http;
using System.Net;
namespace APICatalogo.Application.Responses
{
    public class ActionResponse<T> : IActionResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
