using System.CommandLine;
using ScheduleOfNoticesOfLeasesParser.Ioc;
using ScheduleOfNoticesOfLeasesParser.Service;

namespace ScheduleOfNoticesOfLeasesParser.Commands;

internal class ParserCommand : RootCommand
{
    public ParserCommand() : base("Convert from schedule of notices of leases to a format with entry information extracted")
    {
        var inputFileOption = new Option<string>("--input-file", "input file") { IsRequired = true };
        var outputFileOption = new Option<string>("--output-file", "output file") { IsRequired = true };

        Add(inputFileOption);
        Add(outputFileOption);

        this.SetHandler(async (inputFileValue, outputFileValue, service) =>
        { 
            await service.Parse(inputFileValue, outputFileValue);
        }, inputFileOption, outputFileOption, new IocBinder<IScheduleOfNoticesOfLeaseParserService>());
    }
}
