using System.Linq;
using AutoMapper;
using X.PagedList;

namespace WebApi.Automapper.Convertor
{
    public class
        PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
        where TSource : class where TDestination : class
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source,
            IPagedList<TDestination> destination, ResolutionContext context)
        {
            var vm = source.Select(m
                => context.Mapper.Map<TSource, TDestination>(m)).ToList();

            return new StaticPagedList<TDestination>(vm, source.GetMetaData());
        }
    }
}