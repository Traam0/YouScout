namespace YouScout.Domain.Common.Abstract;

public abstract class Entity
{
    private readonly List<Event> _events = [];
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public Entity RaiseEvent(Event @event)
    {
        _events.Add(@event);
        return this;
    }

    public Entity SafeRaiseEvent(Event @event)
    {
        if (!_events.Contains(@event)) _events.Add(@event);
        return this;
    }

    public Entity WithdrawEvent(Event @event)
    {
        _events.Remove(@event);
        return this;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}