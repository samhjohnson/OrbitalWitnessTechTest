using FluentAssertions;
using ScheduleOfNoticesOfLeasesParser.Mapping;
using ScheduleOfNoticesOfLeasesParser.InputContracts;
using Xunit;
using Entry = ScheduleOfNoticesOfLeasesParser.ResponseContracts.Entry;

namespace ScheduleOfNoticesOfLeasesParser.Tests;

public class MappingTests
{
    [Fact]
    public void ToResponse_WithInputLeaseSchedule_ReturnsCorrectResponse()
    {
        var leaseSchedule = CreateSchedule();

        var response = leaseSchedule.ToResponse();

        response.LeaseSchedule.Should().NotBeNull();
    }

    [Fact]
    public void ToResponse_WithLeaseScheduleItem_ReturnsCorrectResponse()
    {
        var leaseScheduleItem = CreateLeaseScheduleItem();

        var response = leaseScheduleItem.ToResponse();

        response.ScheduleType.Should().Be(leaseScheduleItem.ScheduleType);
    }

    [Fact]
    public void ToResponse_WithScheduleEntry_ReturnsCorrectResponse()
    {
        var scheduleEntry = CreateScheduleEntry("1", "12.12.1995", "EntryType", null);

        var response = scheduleEntry.ToResponse();

        response.EntryNumber.Should().Be(scheduleEntry.EntryNumber);
        response.EntryDate.Should().Be(scheduleEntry.EntryDate);
        response.EntryType.Should().Be(scheduleEntry.EntryType);
    }


    [Theory]
    [MemberData(nameof(EntryTestData))]
    public void ToResponse_WithEntry_ReturnsCorrectResponse(string[] input, Entry expected)
    {
        var actual = input.ToResponse();
        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> EntryTestData()
    {
        yield return new object[]
        {
            new[]
            {
                "28.01.2009      Transformer Chamber (Ground   23.01.2009      EGL551039  ",
                "tinted blue     Floor)                        99 years from              ",
                "(part of)                                     23.1.2009"
            },
            new Entry(
                "28.01.2009 tinted blue (part of)",
                "Transformer Chamber (Ground Floor)",
                "23.01.2009 99 years from 23.1.2009",
                "EGL551039",
                string.Empty)
        };

        yield return new object[]
        {
            new[]
            {
                "26.02.2010      Flat 1703 Landmark West       26.01.2010      EGL568359  ",
                "Edged and       Tower (seventeenth floor      999 years from             ",
                "numbered 4 in   flat)                         1.1.2009                   ",
                "blue (part of)"
            },
            new Entry(
                "26.02.2010 Edged and numbered 4 in blue (part of)",
                "Flat 1703 Landmark West Tower (seventeenth floor flat)",
                "26.01.2010 999 years from 1.1.2009",
                "EGL568359",
                string.Empty)
        };

        yield return new object[]
        {
            new[]
            {
                "15.03.2010      Parking Space 75 Landmark     18.02.2010      EGL569094  ",
                "Edged and       West Tower (basement level    999 years from             ",
                "numbered 25 in  parking space)                1.1.2009                   ",
                "blue on                                                                  ",
                "supplementary                                                            ",
                "plan 1                                                                   ",
                "NOTE: See entry in the Charges Register relating to a Deed of Variation dated 12 October 2015."
            },
            new Entry(
                "15.03.2010 Edged and numbered 25 in blue on supplementary plan 1",
                "Parking Space 75 Landmark West Tower (basement level parking space)",
                "18.02.2010 999 years from 1.1.2009",
                "EGL569094",
                "NOTE: See entry in the Charges Register relating to a Deed of Variation dated 12 October 2015.")
        };

        yield return new object[]
        {
            new[]
            {
                "19.02.1998      95B Denmark Hill (Basement    19.12.1997      TGL143666  ",
                "10 (part of)    Flat)                         125 years from             ",
                "19.12.1997"
            },
            new Entry(
                "19.02.1998 10 (part of)",
                "95B Denmark Hill (Basement Flat)",
                "19.12.1997 125 years from 19.12.1997",
                "TGL143666",
                string.Empty)
        };

        yield return new object[]
        {
            new[]
            {
                "11.06.2007      12 Wymondham Court (Second    26.04.2007      NGL882847  ",
                "Floor Flat)                   From 26/4/2007             ",
                "to 21/11/2174              ",
                "NOTE: The lease was made under the provisions of section 56 or 93(4) of the Leasehold Reform, Housing and Urban Development Act 1993"
            },
            new Entry(
                "11.06.2007",
                "12 Wymondham Court (Second Floor Flat)",
                "26.04.2007 From 26/4/2007 to 21/11/2174",
                "NGL882847",
                "NOTE: The lease was made under the provisions of section 56 or 93(4) of the Leasehold Reform, Housing and Urban Development Act 1993")
        };

        yield return new object[]
        {
            new[]
            {
                "10.04.2008      1 Walsingham (Ground Floor    18.12.2007      NGL895869  ",
                "Flat)                         From                       ",
                "18.12.2007 to              ",
                "21.11.2174                 ",
                "NOTE: The lease was made under the provisions of section 56 or 93(4) of the Leasehold Reform, Housing and Urban Development Act 1993."
            },
            new Entry(
                "11.06.2007",
                "12 Wymondham Court (Second Floor Flat)",
                "26.04.2007 From 26/4/2007 to 21/11/2174",
                "NGL882847",
                "NOTE: The lease was made under the provisions of section 56 or 93(4) of the Leasehold Reform, Housing and Urban Development Act 1993")
        };
    }

    LeaseScheduleRoot CreateSchedule(LeaseScheduleItem? leaseScheduleItem = null)
    {
        return new LeaseScheduleRoot(leaseScheduleItem ?? CreateLeaseScheduleItem());
    }

    LeaseScheduleItem CreateLeaseScheduleItem(string? scheduleType = null, params ScheduleEntry[]? entries)
    {
        return new LeaseScheduleItem(scheduleType ?? "SCHEDULE OF NOTICES OF LEASE",
            entries ?? Array.Empty<ScheduleEntry>());
    }

    ScheduleEntry CreateScheduleEntry(string? entryNumber = null, string? entryDate = null, string? entryType = null, params string[]? entries)
    {
        return new ScheduleEntry(entryNumber, entryDate, entryType, entries ?? Array.Empty<string>());
    }
}