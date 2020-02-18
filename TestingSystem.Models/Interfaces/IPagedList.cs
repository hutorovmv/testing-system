using System.Collections.Generic;

namespace TestingSystem.Models.Interfaces
{
    public interface IPagedList
    {
        int TotalPages { get; }
        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        bool HavePreviousPage { get; }
        bool HaveNextPage { get; }
    }
}