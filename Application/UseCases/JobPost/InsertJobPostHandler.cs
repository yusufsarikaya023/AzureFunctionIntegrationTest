using MediatR;

namespace Application.UseCases.JobPost;

using Domain.Aggregation.JobPost;
using Services;
using Dto;

public record InsertJobPostRequest(InsertJobPostDto Dto) : IRequest<string>;

public class InsertJobPostHandler(IUnitOfWork unitOfWork)
    : Handler(unitOfWork), IRequestHandler<InsertJobPostRequest, string>
{
    public async Task<string> Handle(InsertJobPostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var jobPost = request.Dto.Cast<JobPost>();
            await UnitOfWork.JobPostService().Insert(jobPost!);
            await UnitOfWork.CommitAsync(cancellationToken);
            return "success";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}