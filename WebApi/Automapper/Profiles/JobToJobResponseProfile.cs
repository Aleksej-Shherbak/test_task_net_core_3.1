using AutoMapper;
using Domains;
using WebApi.Responses;

namespace WebApi.Automapper.Profiles
{
    public class JobToJobResponseProfile: Profile
    {
        public JobToJobResponseProfile()
        {
            CreateMap<Job, JobItemResponse>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(x => x.JobId));
        }
    }
}