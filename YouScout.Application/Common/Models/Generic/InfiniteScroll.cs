namespace YouScout.Application.Common.Models.Generic;

public class InfiniteScroll<TModel> where TModel : class
{
    public IReadOnlyList<TModel> Items { get; init; }
    public string? NextCursor { get; init; }
    public bool HasMore { get; init; }

    private InfiniteScroll(
        IReadOnlyList<TModel> items,
        string? nextCursor,
        bool hasMore)
    {
        Items = items;
        NextCursor = nextCursor;
        HasMore = hasMore;
    }

    public static InfiniteScroll<TModel> Create<TCursor>(
        IReadOnlyList<TModel> source,
        int pageSize,
        Func<TModel, TCursor> cursorSelector,
        Func<TCursor, string> encodeCursor)
    {
        var hasMore = source.Count > pageSize;

        var items = hasMore
            ? source.Take(pageSize).ToList()
            : source;

        var nextCursor = items.Count > 0
            ? encodeCursor(cursorSelector(items.Last()))
            : null;

        return new InfiniteScroll<TModel>(items, nextCursor, hasMore);
    }
}