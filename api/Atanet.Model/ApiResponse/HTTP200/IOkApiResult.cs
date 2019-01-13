namespace Atanet.Model.ApiResponse.HTTP200
{
    public interface IOkApiResult : IApiResult
    {
        object Result { get; set; }
    }
}
