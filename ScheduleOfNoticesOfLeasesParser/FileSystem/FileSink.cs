using System.Text.Json;

namespace ScheduleOfNoticesOfLeasesParser.FileSystem;

public interface IFileSink
{
    Task Write<T>(T value, string filename);
}

public class FileFileSink : IFileSink
{
    private readonly JsonSerializerOptions _serializerOptions;

    public FileFileSink(JsonSerializerOptions serializerOptions)
    {
        _serializerOptions = serializerOptions;
    }
    
    public async Task Write<T>(T value, string filename)
    {
        await using FileStream fileStream = File.Create(filename);
        await JsonSerializer.SerializeAsync(fileStream, value, _serializerOptions);
    }
}
