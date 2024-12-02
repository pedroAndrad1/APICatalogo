using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APICatalogo.Domain.Responses
{
    public interface IActionResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
