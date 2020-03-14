using Domains;
using X.PagedList;

namespace WebApi.Responses
{
    public class JobIndexResponse
    {
        public IPagedList<Job> Jobs { get; set; }
        public int TotalItemCount { get; set; }
        public int PageCount { get; set; } 
        public int PageNumber { get; set; } 
        public bool IsLastPage { get; set; } 
        public bool IsFirstPage { get; set; } 
        public bool HasNextPage { get; set; } 
    }
}