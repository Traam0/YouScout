using YouScout.Application.Common.Enums;

namespace YouScout.Application.Common.Interfaces.Strategy;

public interface IFeedStrategyFactory
{
    IFeedStrategy GetStrategy(FeedMode mode);
}