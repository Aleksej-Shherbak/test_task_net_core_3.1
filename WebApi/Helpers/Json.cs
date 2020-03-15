using Domains;
using WebApi.Responses;
using X.PagedList;

namespace WebApi.Helpers
{
    public static class Json
    {
        public static PagedListResponse<T> GeneratePagedListAnswer<T>(IPagedList<T> pagedList)
        {
            return new PagedListResponse<T>()
            {
                PagedList = pagedList,
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