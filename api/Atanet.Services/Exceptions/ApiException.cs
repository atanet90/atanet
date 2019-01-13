namespace Atanet.Services.Exceptions
{
    using System;
    using Atanet.Model.ApiResponse;
    using Atanet.Services.ApiResult;

    public class ApiException : Exception
    {
        public ApiException(IApiResult apiResult) =>
            this.ApiResult = apiResult;

        public IApiResult ApiResult { get; set; }
    }
}
