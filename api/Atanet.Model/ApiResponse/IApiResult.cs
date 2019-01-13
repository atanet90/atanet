namespace Atanet.Model.ApiResponse
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    public interface IApiResult
    {
        HttpStatusCode Code { get; }

        bool Success { get; }

        string Message { get; set; }

        IActionResult GetResultObject();
    }
}
