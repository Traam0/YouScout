namespace YouScout.Application.Posts.Exceptions;

public class UnsupportedVideoFormatException(string contentType)
    : Exception($"Video format '{contentType}' is not supported.");