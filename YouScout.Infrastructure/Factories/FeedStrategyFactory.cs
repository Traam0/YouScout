using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using YouScout.Application.Common.Enums;
using YouScout.Application.Common.Interfaces.Strategy;

namespace YouScout.Infrastructure.Factories;

public class FeedStrategyFactory(IServiceProvider serviceProvider) : IFeedStrategyFactory
{
    public IFeedStrategy GetStrategy(FeedMode mode)
    {
        return mode switch
        {
            FeedMode.ForYou => serviceProvider.GetRequiredKeyedService<IFeedStrategy>(nameof(FeedMode.ForYou)),
            FeedMode.Explore => serviceProvider.GetRequiredKeyedService<IFeedStrategy>(nameof(FeedMode.Explore)),
            FeedMode.Following => serviceProvider.GetRequiredKeyedService<IFeedStrategy>(nameof(FeedMode.Following)),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}