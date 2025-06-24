using System;
using System.Text.Json;

namespace TimeSplitter
{
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine($"{GetTimeJson()}");
        }

	public static string GetCentralTimeZoneId()
        {
            if (OperatingSystem.IsWindows())
                return "Central Standard Time"; // Windows ID
            else if (OperatingSystem.IsLinux())
                return "America/Chicago"; // IANA ID for Linux
            else
                throw new PlatformNotSupportedException("Unsupported OS for time zone conversion.");
        }

        public static string GetTimeJson()
        {
            var utcNow = DateTime.UtcNow;
            var centralZone = TimeZoneInfo.FindSystemTimeZoneById(GetCentralTimeZoneId());
            var central = TimeZoneInfo.ConvertTimeFromUtc(utcNow, centralZone);
            string zone = centralZone.IsDaylightSavingTime(central) ? "CDT" : "CST";
            var payload = new
            {
                time = "splitter",
                utc = utcNow,
                central = central,
                zone = zone,
                diff = utcNow - central
            };

            return JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
