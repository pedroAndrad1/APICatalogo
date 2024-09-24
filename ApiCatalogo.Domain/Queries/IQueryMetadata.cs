﻿namespace APICatalogo.Domain.Queries
{
    public interface IQueryMetadata
    {
        public int TotalCount { get; init; }
        public int PageSize { get; init; }
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
        public bool HasNext { get; init; }
        public bool HasPrevious { get; init; }
    }
}
