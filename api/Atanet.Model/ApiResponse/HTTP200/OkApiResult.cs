namespace Atanet.Model.ApiResponse.HTTP200
{
    using System.Net;

    public class OkApiResult : ApiResultBase, IOkApiResult
    {
        public OkApiResult(object obj) =>
            this.Result = obj;

        public object Result { get; set; }

        public override HttpStatusCode Code => HttpStatusCode.OK;

        public override bool Success => true;

        public override string Message { get; set; } = "Success";

        public override object GetJsonObject()
        {
            if (this.Result != null)
            {
                return this.Result;
            }

            return new
            {
                Success = this.Success,
                Message = this.Message
            };
        }
    }
}
