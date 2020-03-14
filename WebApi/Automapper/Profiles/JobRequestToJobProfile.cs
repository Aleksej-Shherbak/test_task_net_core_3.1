using AutoMapper;
using Domains;
using WebApi.Requests;

namespace WebApi.Automapper.Profiles
{
    public class JobRequestToJobProfile: Profile
    {
        public JobRequestToJobProfile()
        {
            CreateMap<JobRequest, Job>()
                .ForMember(dest => dest.JobId,
                    opt =>
                        opt.MapFrom(x => x.Id));
        }
    }
}