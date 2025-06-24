namespace TimeSplitter.Tests;

using System;
using System.Text.Json;
using Xunit;
using TimeSplitter;

public class UnitTest1
{
    [Fact]
    public void GetTimeJson_ReturnsValidJson()
    {
        string json = Program.GetTimeJson();

        // Assert it's not null or empty
        Assert.False(string.IsNullOrWhiteSpace(json));

        // Parse JSON
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Check 'time' property
        Assert.True(root.TryGetProperty("time", out var timeProp));
        Assert.Equal("splitter", timeProp.GetString());

        // Check 'utc' property is a valid datetime string
        Assert.True(root.TryGetProperty("utc", out var utcProp));
        Assert.True(DateTime.TryParse(utcProp.GetString(), out var utcDate));

        // Check 'central' property is a valid datetime string
        Assert.True(root.TryGetProperty("central", out var centralProp));
        Assert.True(DateTime.TryParse(centralProp.GetString(), out var centralDate));

        // Check 'zone' property is either "CST" or "CDT"
        Assert.True(root.TryGetProperty("zone", out var zoneProp));
        string? zone = zoneProp.GetString();
        Assert.False(string.IsNullOrEmpty(zone));
        Assert.True(zone == "CST" || zone == "CDT");

        // Check 'diff' property is a valid timespan string
        Assert.True(root.TryGetProperty("diff", out var diffProp));
        Assert.True(TimeSpan.TryParse(diffProp.GetString(), out var diff));

        // Additional sanity check: diff should be positive (hours difference)
        Assert.True(diff.TotalHours > 0);
    }
}
