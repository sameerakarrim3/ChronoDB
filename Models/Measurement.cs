namespace ChronoDB.Models;

/// <summary>A named numeric value recorded at a specific point in time.</summary>
public sealed record Measurement(string Name, double Value, DateTimeOffset Timestamp);
