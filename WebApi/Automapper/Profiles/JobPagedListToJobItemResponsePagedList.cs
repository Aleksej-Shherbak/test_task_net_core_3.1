using AutoMapper;
using Domains;
using WebApi.Automapper.Convertor;
using WebApi.Responses;
using X.PagedList;

namespace WebApi.Automapper.Profiles
{
    public class JobPagedListToJobItemResponsePagedList: Profile
    {
        public JobPagedListToJobItemResponsePagedList()
        {
            CreateMap<IPagedList<Job>, IPagedList<JobItemResponse>>()
                .ConvertUsing<PagedListConverter<Job, JobItemResponse>>();

        }
    }
}