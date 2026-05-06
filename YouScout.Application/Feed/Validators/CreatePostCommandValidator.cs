using FluentValidation;
using Microsoft.AspNetCore.Http;
using YouScout.Application.Feed.Commands;

namespace YouScout.Application.Feed.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    // TODO_START 
    // TODO Merge these properties into a single record::VideoUploadOptions and have it injected as IOption<VideoUploadOptions>
    private const long MaxFileSizeBytes = 100 * 1024 * 1024;
    private static readonly string[] AllowedVideoExtensions = [".mp4", ".mov", ".avi", ".mkv"];
    // TODO_END

    public CreatePostCommandValidator()
    {
        RuleFor(c => c.Video)
            .NotNull().WithMessage("${PropertyName} is required.")
            .Must(CreatePostCommandValidator.BeValidFileSize)
            .WithMessage($"Video must not exceed {CreatePostCommandValidator.MaxFileSizeBytes / (1024 * 1024)} MB.")
            .Must(CreatePostCommandValidator.HaveValidExtension)
            .WithMessage("Invalid Video Format.");

        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("${PropertyName} must not exceed 500 characters.")
            .When(d => !string.IsNullOrWhiteSpace(d.Description));

        RuleFor(c => c.Hashtags)
            .NotNull().WithMessage("${PropertyName} is required.")
            .Must(CreatePostCommandValidator.AllValidHashtags).WithMessage("Invalid Hashtags.");

        RuleFor(c => c.Skills)
            .NotNull().WithMessage("${PropertyName} is required.")
            .Must(s =>
            {
                var skills = s.ToList();
                return skills.Distinct().Count() == skills.Count;
            })
            .WithMessage("Duplicate skills are not allowed.");
    }

    private static bool BeValidFileSize(IFormFile file)
    {
        return file.Length is > 0 and <= MaxFileSizeBytes;
    }

    private static bool HaveValidExtension(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return AllowedVideoExtensions.Contains(extension);
    }

    private static bool AllValidHashtags(IEnumerable<string> hashtags)
    {
        return hashtags.All(h =>
            !string.IsNullOrWhiteSpace(h) &&
            h.StartsWith($"#") &&
            h.Length <= 50 &&
            h.Skip(1).All(char.IsLetterOrDigit)
        );
    }
}