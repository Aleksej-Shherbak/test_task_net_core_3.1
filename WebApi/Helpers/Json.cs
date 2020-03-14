using Domains;
using WebApi.Responses;
using X.PagedList;

namespace WebApi.Helpers
{
    public static class Json
    {
        public static JobIndexResponse GeneratePagedListAnswer(IPagedList<Job> pagedList)
        {
            return new JobIndexResponse()
            {
                Jobs = pagedList,
                TotalItemCount = pagedList.TotalItemCount,
                PageCount = pagedList.PageCount,
                PageNumber = pagedList.PageNumber,
                IsLastPage = pagedList.IsLastPage,
                IsFirstPage = pagedList.IsFirstPage,
                HasNextPage = pagedList.HasNextPage,
            };
        }
        

    }
}