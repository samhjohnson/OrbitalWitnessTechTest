using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using ScheduleOfNoticesOfLeasesParser.Commands;

return await new CommandLineBuilder(new ParserCommand())
    .UseDefaults()
    .UseExceptionHandler((ex, ic) =>
    {
        ic.Console.Error.WriteLine(ex.Message);
        ic.ExitCode = -1;
    })
    .Build()
    .InvokeAsync(args);


