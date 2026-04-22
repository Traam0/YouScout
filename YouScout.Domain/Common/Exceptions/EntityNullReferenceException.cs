namespace YouScout.Domain.Common.Exceptions;

public class EntityNullReferenceException<TEntity> : Exception where TEntity : class
{
    public EntityNullReferenceException(object id) : base(
        $"{typeof(TEntity).Name} with the id: {id} does not or no longer exists.")
    {
    }

    public EntityNullReferenceException(object id, Exception innerException) : base(
        $"{typeof(TEntity).Name} with the id: {id} does not or no longer exists.", innerException)
    {
    }

    public EntityNullReferenceException(string identifier, string identifierName) : base(
        $"{typeof(TEntity).Name} with ({identifierName}: {identifier}) does not or no longer exists.")
    {
    }

    public EntityNullReferenceException(string identifier, string identifierName, Exception innerException) : base(
        $"{typeof(TEntity).Name} with ({identifierName}: {identifier}) does not or no longer exists.", innerException)
    {
    }
}