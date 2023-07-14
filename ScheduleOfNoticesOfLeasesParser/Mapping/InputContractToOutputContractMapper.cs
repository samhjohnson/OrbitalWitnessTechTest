using ScheduleOfNoticesOfLeasesParser.InputContracts;
using ScheduleOfNoticesOfLeasesParser.ResponseContracts;
using static System.String;
using InputLeaseScheduleItem = ScheduleOfNoticesOfLeasesParser.InputContracts.LeaseScheduleItem;
using InputScheduleEntry = ScheduleOfNoticesOfLeasesParser.InputContracts.ScheduleEntry;
using LeaseScheduleRoot = ScheduleOfNoticesOfLeasesParser.ResponseContracts.LeaseScheduleRoot;
using ResponseLeaseScheduleItem = ScheduleOfNoticesOfLeasesParser.ResponseContracts.LeaseScheduleItem;
using ResponseScheduleEntry = ScheduleOfNoticesOfLeasesParser.ResponseContracts.ScheduleEntry;

namespace ScheduleOfNoticesOfLeasesParser.Mapping;

public static class MappingExtensions
{
    public static LeaseScheduleRoot ToResponse(this InputContracts.LeaseScheduleRoot tFrom)
    {
        return new LeaseScheduleRoot(tFrom.LeaseSchedule.ToResponse());
    }
    internal static ResponseLeaseScheduleItem ToResponse(this InputLeaseScheduleItem source)
    {
        return new ResponseLeaseScheduleItem(source.ScheduleType, source.ScheduleEntry.Select(x => x.ToResponse()).ToArray());
    }

    internal static ResponseScheduleEntry ToResponse(this InputScheduleEntry source)
    {
        return new ResponseScheduleEntry(source.EntryNumber, source.EntryDate, source.EntryType, source.EntryText.ToResponse());
    }

    internal static Entry ToResponse(this string[] entryText)
    {
        List<string> registrations = new List<string>();
        List<string> propertyDescriptions = new List<string>();
        List<string> dateOfLeases = new List<string>();
        List<string> lesseeTitles = new List<string>();
        List<string> notes = new List<string>();

        bool seenNotes = false;
        foreach (var entry in entryText.Where(x => x is not null))
        {
            seenNotes = !seenNotes && entry.Contains("NOTE");
            if (seenNotes)
            {
                notes.Add(entry.Trim());
                continue;
            }

            registrations.AddIfNotEmpty(entry.GetRegistration());
            propertyDescriptions.AddIfNotEmpty(entry.GetPropertyDescription());
            dateOfLeases.AddIfNotEmpty(entry.GetDateOfLease());
            lesseeTitles.AddIfNotEmpty(entry.GetLesseeTitle());
        }

        var registration = Join(" ", registrations);
        var propertyDescription = Join(" ", propertyDescriptions);
        var dateOfLease = Join(" ", dateOfLeases);
        var lesseeTitle = Join(" ", lesseeTitles);
        var note  = Join(" ", notes);

        return new Entry(registration, propertyDescription, dateOfLease, lesseeTitle, note);
    }

    internal static void AddIfNotEmpty(this IList<string> strs, string str)
    {
        if (!str.Equals(Empty))
        {
            strs.Add(str);
        }
    }

    internal static string GetRegistration(this string entry)
    {
        return GetTrimmedSubStringOrEmpty(entry, 0, 16);
    }

    internal static string GetPropertyDescription(this string entry)
    {
        return GetTrimmedSubStringOrEmpty(entry, 16, 29);
    }
    internal static string GetDateOfLease(this string entry)
    {
        return GetTrimmedSubStringOrEmpty(entry, 46, 16);
    }
    internal static string GetLesseeTitle(this string entry)
    {
        return GetTrimmedSubStringOrEmpty(entry, 62, 11);
    }

    private static string GetTrimmedSubStringOrEmpty(this string entry, int startPosition, int length)
    {
        return entry.Length >= startPosition
            ? entry.Substring(startPosition, Math.Min(length, Math.Max(0, entry.Length - startPosition))).Trim() 
            : Empty;
    }
}