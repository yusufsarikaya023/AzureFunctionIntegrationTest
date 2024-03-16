using AutoMapper;

namespace Application.UseCases.JobPost.Dto;

using Domain.Aggregation.JobPost;

public class JobPostDtoMapper : Profile
{
    public JobPostDtoMapper()
    {
        CreateMap<InsertJobPostDto, JobPost>();
    }
}