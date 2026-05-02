namespace YouScout.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public string EntityName { get; }
    public string IdentifierName { get; }
    public object Identifier { get; }

    private NotFoundException(string entityName, object identifier, string identifierName, Exception? inner = null)
        : base($"{entityName} with ({identifierName}: {identifier}) does not or no longer exist.", inner)
    {
        EntityName = entityName;
        Identifier = identifier;
        IdentifierName = identifierName;
    }

    public static NotFoundException For<TEntity>(object id)
        where TEntity : class
        => new(typeof(TEntity).Name, id, "Id");

    public static NotFoundException For<TEntity>(object identifier, string identifierName)
        where TEntity : class
        => new(typeof(TEntity).Name, identifier, identifierName);

    public static NotFoundException For<TEntity>(object id, Exception inner)
        where TEntity : class
        => new(typeof(TEntity).Name, id, "Id", inner);
}