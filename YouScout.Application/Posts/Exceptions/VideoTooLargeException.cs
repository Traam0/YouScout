namespace YouScout.Application.Posts.Exceptions;

public class VideoTooLargeException(long maxBytes)
    : Exception($"Video exceeds the maximum allowed size of {maxBytes / 1_048_576} MB.");