using System.Net;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Test;

public class FakeHttpRequestData : HttpRequestData
{
    public FakeHttpRequestData(FunctionContext functionContext, Stream body) : base(
        functionContext)
    {
        Body = body;
        Headers = new HttpHeadersCollection { { "Content-Type", "application/json; charset=utf-8" } };
    }

    public override Stream Body { get; }
    public override HttpHeadersCollection Headers { get; }
    public override IReadOnlyCollection<IHttpCookie> Cookies { get; }
    public override Uri Url { get; }
    public override IEnumerable<ClaimsIdentity> Identities { get; }
    public override string Method { get; }

    public override HttpResponseData CreateResponse()
    {
        return new FakeHttpResponseData(FunctionContext);
    }
}

public class FakeHttpResponseData : HttpResponseData
{
    public FakeHttpResponseData(FunctionContext functionContext) : base(functionContext)
    {
        Headers = new HttpHeadersCollection();
        Body = new MemoryStream();
    }


    public override HttpStatusCode StatusCode { get; set; }
    public override HttpHeadersCollection Headers { get; set; }
    public override Stream Body { get; set; }
    public override HttpCookies Cookies { get; }
}