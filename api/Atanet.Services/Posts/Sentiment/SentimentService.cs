namespace Atanet.Services.Posts.Sentiment
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Atanet.Model.Dto.Sentiment;
    using Atanet.Services.ApiResult;
    using Atanet.Services.Exceptions;
    using Newtonsoft.Json;

    public class SentimentService : ISentimentService
    {
        private readonly string host = Environment.GetEnvironmentVariable("SENTIMENT_HOST");

        private readonly string port = Environment.GetEnvironmentVariable("SENTIMENT_PORT");

        private readonly IApiResultService apiResultService;

        public SentimentService(IApiResultService apiResultService)
        {
            this.apiResultService = apiResultService;
        }

        public float GetSentiment(string sentence)
        {
            var httpClient = new HttpClient();
            var uri = new Uri($"http://{this.host}:{this.port}?text={sentence}");
            var sentimentResult = httpClient.GetAsync(uri).Result;
            var textResponse = sentimentResult.Content.ReadAsStringAsync().Result;
            switch (sentimentResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    var model = JsonConvert.DeserializeObject<SentimentModel>(textResponse);
                    return model.Sentiment;

                case HttpStatusCode.BadRequest:
                    var errorResponse = JsonConvert.DeserializeObject<SentimentAnalysisError>(textResponse);
                    throw new ApiException(this.apiResultService.BadRequestResult(errorResponse.Message));

                case HttpStatusCode.InternalServerError:
                    throw new ApiException(this.apiResultService.InternalServerErrorResult(null));
            }

            throw new ApiException(this.apiResultService.InternalServerErrorResult(null));
        }
    }
}