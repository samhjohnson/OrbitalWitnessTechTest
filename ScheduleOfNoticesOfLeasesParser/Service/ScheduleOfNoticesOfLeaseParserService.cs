using ScheduleOfNoticesOfLeasesParser.FileSystem;
using ScheduleOfNoticesOfLeasesParser.InputContracts;
using ScheduleOfNoticesOfLeasesParser.Mapping;

namespace ScheduleOfNoticesOfLeasesParser.Service;

internal interface IScheduleOfNoticesOfLeaseParserService
{
    Task Parse(string inputFilename, string outputFilename);
}

internal class ScheduleOfNoticesOfLeaseParserService : IScheduleOfNoticesOfLeaseParserService
{
    private readonly IFileSource _fileSource;
    private readonly IFileSink _fileSink;

    public ScheduleOfNoticesOfLeaseParserService(
        IFileSource fileSource,
        IFileSink fileSink)
    {
        _fileSource = fileSource;
        _fileSink = fileSink;
    }

    public async Task Parse(string inputFilename, string outputFilename)
    {
        var inputLeaseSchedules = await _fileSource.Read<LeaseScheduleRoot[]>(inputFilename);

        var responseLeaseSchedules = inputLeaseSchedules.Select(x => x.ToResponse());

        await _fileSink.Write(responseLeaseSchedules, outputFilename);
    }
}