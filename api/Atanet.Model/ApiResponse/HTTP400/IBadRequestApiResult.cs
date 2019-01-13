namespace Atanet.Model.ApiResponse.HTTP400
{
    using System.Collections.Generic;
    using Atanet.Model.Validation;

    public interface IBadRequestApiResult : IApiResult
    {
        IDictionary<ErrorCode, ErrorDefinition> Errors { get; set; }
    }
}
