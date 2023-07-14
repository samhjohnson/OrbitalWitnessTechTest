namespace ScheduleOfNoticesOfLeasesParser.ResponseContracts;

public record LeaseScheduleRoot(LeaseScheduleItem LeaseSchedule);

public record LeaseScheduleItem(string ScheduleType, ScheduleEntry[] ScheduleEntry);

public record ScheduleEntry(string EntryNumber, string EntryDate, string EntryType, Entry Entry);

public record Entry(string RegistrationDateAndPlanRef, string PropertyDescription, string DateOfLeaseAndTerm, string LesseesTitle, string? Note);