namespace ScheduleOfNoticesOfLeasesParser.InputContracts;

public record LeaseScheduleRoot(LeaseScheduleItem LeaseSchedule);

public record LeaseScheduleItem(string ScheduleType, ScheduleEntry[] ScheduleEntry);

public record ScheduleEntry(string EntryNumber, string EntryDate, string EntryType, string[] EntryText);

