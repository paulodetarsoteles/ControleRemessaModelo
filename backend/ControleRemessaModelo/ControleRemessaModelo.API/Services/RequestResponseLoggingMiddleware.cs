using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;

namespace ControleRemessaModelo.API.Services
{
    public class RequestResponseLoggingMiddleware(RequestDelegate next, IMongoClient mongoClient, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly IMongoClient _mongoClient = mongoClient;
        private readonly string _databaseName = "RequestLogs";

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            Stream? originalBodyStream = context.Response.Body;
            MemoryStream responseBody = new();
            context.Response.Body = responseBody;

            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(responseBody, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true).ReadToEndAsync();

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;

            await LogRequestAndResponseAsync(context, requestBody, responseBodyText);
        }

        private async Task LogRequestAndResponseAsync(HttpContext context, string requestBody, string responseBodyText)
        {
            BsonDocument requestLog = new()
            {
                { "RequestPath", context.Request.Path.ToString() },
                { "RequestBody", requestBody },
                { "Timestamp", DateTime.UtcNow }
            };

            BsonDocument responseLog = new()
            {
                { "RequestPath", context.Request.Path.ToString() },
                { "ResponseBody", responseBodyText },
                { "Timestamp", DateTime.UtcNow }
            };

            string requestCollectionName = $"{context.Request.Method}_Input_{context.Request.Path.ToString().Replace('/', '.')}";
            string responseCollectionName = $"{context.Request.Method}_Output_{context.Request.Path.ToString().Replace('/', '.')}";

            IMongoDatabase database = _mongoClient.GetDatabase(_databaseName);
            IMongoCollection<BsonDocument> requestCollection = database.GetCollection<BsonDocument>(requestCollectionName);
            IMongoCollection<BsonDocument> responseCollection = database.GetCollection<BsonDocument>(responseCollectionName);

            await requestCollection.InsertOneAsync(requestLog);
            await responseCollection.InsertOneAsync(responseLog);
        }
    }
}
