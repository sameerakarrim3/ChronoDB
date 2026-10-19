using System.Text.Json;
using ChronoDB.Models;

namespace ChronoDB.Storage;

/// <summary>Reads and writes the local JSON file used by the learning prototype.</summary>
public sealed class JsonMeasurementStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public JsonMeasurementStore(string? filePath = null)
    {
        FilePath = filePath ?? Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory, "..", "..", "..", "measurements.json"));
    }

    public string FilePath { get; }

    public List<Measurement> Load()
    {
        if (!File.Exists(FilePath))
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Measurement>>(json) ?? [];
        }
        catch (JsonException)
        {
            Console.WriteLine("The saved data file could not be read, so starting with an empty list.");
            return [];
        }
    }

    public void Save(IReadOnlyCollection<Measurement> measurements)
    {
        var json = JsonSerializer.Serialize(measurements, JsonOptions);
        File.WriteAllText(FilePath, json);
    }
}
