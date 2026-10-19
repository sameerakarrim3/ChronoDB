# Architecture

## Current data flow

```text
User input
   │
   ▼
ChronoDbRepl (Cli)
   │  creates and queries Measurement values
   ▼
List<Measurement> (in memory during the current process)
   │
   ▼
JsonMeasurementStore (Storage)
   │  reads at startup; rewrites after each add
   ▼
measurements.json (local project data, ignored by Git)
```

## Main parts

- **`Models/Measurement.cs`** describes one metric name, numeric value, and timestamp.
- **`Cli/ChronoDbRepl.cs`** owns the interactive loop, input checks, and the `add`, `list`, `avg`, and `help` commands.
- **`Storage/JsonMeasurementStore.cs`** loads and saves the collection as indented JSON.
- **`Program.cs`** wires storage and the command prompt together.

## Query behavior

`avg <metric>` filters the in-memory collection by metric name without regard to letter case, then calculates the arithmetic mean of the matching values. It scans the collection, so query time grows linearly with the number of saved measurements.

## Persistence behavior

The JSON file is loaded once at startup. After each successful `add`, the entire collection is serialized and written to disk. This keeps the first version easy to inspect, but it is not an append-only log and does not provide crash-safe transactional guarantees.

## Planned evolution

The roadmap introduces time-range filtering and a small parser before custom indexing and binary storage. Each step should keep the current command-line behavior understandable and make the storage tradeoffs visible.
