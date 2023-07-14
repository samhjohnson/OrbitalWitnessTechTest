using System.Text.Json;

namespace ScheduleOfNoticesOfLeasesParser.FileSystem;

interface IFileSource
{
    Task<T> Read<T>(string filename);
}

internal class FileFileSource : IFileSource
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public FileFileSource(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public async Task<T> Read<T>(string filename)
    {
        using var streamReader = new StreamReader(filename);
        await streamReader.ReadToEndAsync();
        streamReader.BaseStream.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(streamReader.BaseStream, _jsonSerializerOptions);
    }
}