using System.Linq.Expressions;
using System.Text.Json;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace YouScout.Infrastructure.Data.Converters;

public class StringListConverter() : ValueConverter<List<string>, string>(
    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());