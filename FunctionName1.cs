using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Company.Function
{
    public class FunctionName1
    {
        [Function("PostToMe")]
        public async Task<DualWebAndBlobResponse> WriteWebhookBody(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var webhookContents = string.Empty;
            try
            {
                webhookContents = await new StreamReader(req.Body).ReadToEndAsync();
                response.WriteString("Done-ski");
            }
            catch(Exception ex)
            {
                response.WriteString($"Well shit: {ex.ToString()}");
            }

            return new DualWebAndBlobResponse()
            {
                Contents = webhookContents,
                HttpResponse = response
            };
        }

        [Function("ReadAndClear")]
        public DualWebAndBlobResponse ReadAndClear(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            [BlobInput("myfunctionapp9fd8/captain.hook")] string myBlob
            )
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            
            response.WriteString(myBlob);
            
            return new DualWebAndBlobResponse()
            {
                Contents = string.Empty,
                HttpResponse = response
            };
            
        }
    }

    public class DualWebAndBlobResponse
    {
        [BlobOutput("myfunctionapp9fd8/captain.hook", Connection = "AzureWebJobsStorage")]
        public string Contents { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
}
