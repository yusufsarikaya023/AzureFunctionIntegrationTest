using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Function;

public abstract class Abstraction(IMediator mediator)
{
    protected async Task<HttpResponseData> PostResponse(HttpRequestData req, IRequest request)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await mediator.Send(request);
        return response;
    }
    
    protected async Task<HttpResponseData> PostResponse<TResponse>(HttpRequestData req, IRequest<TResponse> request) 
    {
        try
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var result =  await mediator.Send(request);
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var options = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
            // check if it json convertable
            var value = result switch
            {
                string s => s,
                int i => i.ToString(),
                _ => JsonConvert.SerializeObject(result, options)
            };
            
            await  response.WriteStringAsync(value);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}