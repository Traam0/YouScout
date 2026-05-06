namespace YouScout.Application.Common.Enums;

[Flags]
public enum FeedMode
{
    None = 0,
    ForYou = 1 << None,
    Following = ForYou << 1,
    Explore = Following << 1,

    Hybrid = ForYou | Following,
    All = ForYou | Following | Explore,
}