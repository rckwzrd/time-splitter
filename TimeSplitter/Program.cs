using System;
using System.Text.Json;

namespace TimeSplitter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"{GetTimeJson()}");
        }

	static string GetCentralTimeZoneId()
        {
            if (OperatingSystem.IsWindows())
                return "Central Standard Time"; // Windows ID
            else if (OperatingSystem.IsLinux())
                return "America/Chicago"; // IANA ID for Linux
            else
                throw new PlatformNotSupportedException("Unsupported OS for time zone conversion.");
        }

        static string GetTimeJson()
        {
            var utcNow = DateTime.UtcNow;
            var centralZone = TimeZoneInfo.FindSystemTimeZoneById(GetCentralTimeZoneId());
            var central = TimeZoneInfo.ConvertTimeFromUtc(utcNow, centralZone);

            var payload = new
            {
                time = "splitter",
                utc = utcNow,
                central = central,
                zone = centralZone.IsDaylightSavingTime(central) ? "CDT" : "CST",
                diff = utcNow - central
            };

            return JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
