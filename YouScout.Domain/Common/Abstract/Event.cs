using MediatR;

namespace YouScout.Domain.Common.Abstract;

public abstract class Event : INotification
{
    public Guid Id { get; set; } =  Guid.NewGuid();
}