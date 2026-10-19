using System.Globalization;
using ChronoDB.Models;
using ChronoDB.Storage;

namespace ChronoDB.Cli;

/// <summary>Runs ChronoDB's beginner-friendly command prompt.</summary>
public sealed class ChronoDbRepl
{
    private readonly JsonMeasurementStore _store;

    public ChronoDbRepl(JsonMeasurementStore store)
    {
        _store = store;
    }

    public void Run()
    {
        var measurements = _store.Load();

        Console.WriteLine("ChronoDB — tiny time-series database");
        Console.WriteLine($"Loaded {measurements.Count} measurement(s).");
        Console.WriteLine("Commands: add, list, avg <metric>, help, quit");

        while (true)
        {
            Console.Write("\n> ");
            var command = Console.ReadLine()?.Trim();

            if (command is null)
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            var parts = command.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var name = parts[0].ToLowerInvariant();
            var argument = parts.Length == 2 ? parts[1].Trim() : "";

            switch (name)
            {
                case "add":
                    AddMeasurement(measurements);
                    break;
                case "list":
                    ListMeasurements(measurements);
                    break;
                case "avg":
                    ShowAverage(measurements, argument);
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "quit":
                case "exit":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Unknown command. Type help to see the available commands.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    private void AddMeasurement(List<Measurement> measurements)
    {
        Console.Write("Metric name (for example, cpu): ");
        var metricName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(metricName))
        {
            Console.WriteLine("The metric name cannot be empty.");
            return;
        }

        Console.Write("Value (for example, 23.5): ");
        var input = Console.ReadLine();
        if (!TryParseValue(input, out var value))
        {
            Console.WriteLine("Please enter a number, such as 23.5.");
            return;
        }

        var measurement = new Measurement(metricName, value, DateTimeOffset.Now);
        measurements.Add(measurement);
        _store.Save(measurements);
        Console.WriteLine($"Added and saved {measurement.Name} = {measurement.Value}.");
    }

    private static bool TryParseValue(string? input, out double value)
    {
        return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value)
            || double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
    }

    private static void ListMeasurements(IReadOnlyCollection<Measurement> measurements)
    {
        if (measurements.Count == 0)
        {
            Console.WriteLine("No measurements yet. Type add to create one.");
            return;
        }

        foreach (var measurement in measurements)
        {
            Console.WriteLine($"{measurement.Timestamp:yyyy-MM-dd HH:mm:ss}  {measurement.Name} = {measurement.Value}");
        }
    }

    private static void ShowAverage(IReadOnlyCollection<Measurement> measurements, string metricName)
    {
        if (string.IsNullOrWhiteSpace(metricName))
        {
            Console.WriteLine("Usage: avg <metric> (for example, avg cpu)");
            return;
        }

        var values = measurements
            .Where(measurement => string.Equals(measurement.Name, metricName, StringComparison.OrdinalIgnoreCase))
            .Select(measurement => measurement.Value)
            .ToList();

        if (values.Count == 0)
        {
            Console.WriteLine($"No measurements found for '{metricName}'.");
            return;
        }

        Console.WriteLine($"Average {metricName} = {values.Average()}");
    }

    private static void ShowHelp()
    {
        Console.WriteLine("add             Record a metric name and numeric value");
        Console.WriteLine("list            Show every saved measurement");
        Console.WriteLine("avg <metric>    Calculate a metric's average (for example, avg cpu)");
        Console.WriteLine("help            Show this command list");
        Console.WriteLine("quit            Close ChronoDB");
    }
}
