using System.Collections.Generic;
using TestingSystem.Models.Entities;
using AutoMapper;

namespace TestingSystem.BLL.Utils
{
    public static class Extensions
    {
        public static PagedList<TDest> ConvertPagedList<TSrc, TDest>(this PagedList<TSrc> src, IMapper mapper)
        {
            IEnumerable<TDest> destItems = mapper.Map<IEnumerable<TDest>>(src.Items);
            return new PagedList<TDest>(destItems, src.TotalCount, src.PageSize, src.PageIndex);
        }
    }
}
