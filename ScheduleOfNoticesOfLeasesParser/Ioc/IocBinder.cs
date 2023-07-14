using System.CommandLine.Binding;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ScheduleOfNoticesOfLeasesParser.FileSystem;
using ScheduleOfNoticesOfLeasesParser.Service;

namespace ScheduleOfNoticesOfLeasesParser.Ioc;

internal class IocBinder<T> : BinderBase<T>
{
    protected override T GetBoundValue(BindingContext bindingContext)
    {
        return new ServiceCollection()
            .AddSingleton<IScheduleOfNoticesOfLeaseParserService, ScheduleOfNoticesOfLeaseParserService>()
            .AddSingleton<IFileSource, FileFileSource>()
            .AddSingleton<IFileSink, FileFileSink>()
            .AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            })
            .BuildServiceProvider()
            .GetRequiredService<T>();
    }
}