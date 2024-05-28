using System.Net;
using System.Text;
using Application.UseCases.JobPost.Dto;
using Function.Functions;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Test.IntegrationTest.JobPost;

[Collection("Factory collection")]
public class InsertJobPostFunctionTest2(FunctionApplicationStartup startup)
{
    private readonly InsertJobPostFunction _sut = new(startup.host.Services.GetRequiredService<IMediator>());

    [Fact]
    public async Task Insert_JobPost_Should_Be_Added()
    {
        // arrange
        var dto = new InsertJobPostDto
        {
            Title = "Software Engineer",
            Description = "We are looking for a software engineer",
        };
        var context = new FakeFunctionContext();

        var req = new FakeHttpRequestData(context,
            new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)))
        );

        // act
        var result = await _sut.Run(req, context);
        result.Body.Position = 0; 
        var reader = new StreamReader(result.Body);
        var text = await reader.ReadToEndAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal("success", text);
    }
}