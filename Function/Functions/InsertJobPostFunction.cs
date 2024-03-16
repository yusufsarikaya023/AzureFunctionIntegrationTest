using System.Net;
using Application.UseCases.JobPost;
using Application.UseCases.JobPost.Dto;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace Function.Functions;

public class InsertJobPostFunction(IMediator mediator): Abstraction(mediator)
{
    [Function("InsertJobPost")]
    [OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, Scheme = OpenApiSecuritySchemeType.Bearer,
        BearerFormat = "JWT")]
    [OpenApiOperation(operationId: nameof(InsertJobPostFunction), tags: new[] { "JobPost" },
        Summary = "Add New Job Post", Description = "Operation Insert new job post to database.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InsertJobPostDto), Required = true,
        Description = "Give new job post to database.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
        Summary = "Job post id.", Description = "job post id")]
    
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext) => 
        await PostResponse(
            req, new InsertJobPostRequest(req.Convert<InsertJobPostDto>())
        );
}