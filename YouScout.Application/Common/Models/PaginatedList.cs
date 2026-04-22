namespace YouScout.Application.Common.Models;

public class PaginatedList<T> where T : class
{
    public IReadOnlyCollection<T> Items { get; private set; }
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalItems { get; set; }
    public int Size { get; set; }


    private PaginatedList(IReadOnlyCollection<T> items, int pageNumber, int pageSize, int totalItems)
    {
        this.Items = items;
        this.PageNumber = pageNumber;
        this.TotalItems = totalItems;
        this.Size = pageSize;
        this.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static PaginatedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        IEnumerable<T> enumerable = source as T[] ?? source.ToArray();
        var count = enumerable.Count();
        if (pageNumber < 1) throw new ArgumentException($"minimum allowed page number is one, {pageNumber} was given.");
        if (pageSize < 1) throw new ArgumentException($"minimum allowed page size is one, {pageSize} was given.");

        var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, pageNumber, pageSize, count);
    }

    public bool HasNextPage => this.PageNumber < this.TotalPages;
    public bool HasPrevPage => this.PageNumber > 1;
}