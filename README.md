# ChronoDB

**A small time-series database prototype in C#**

ChronoDB is a from-scratch learning project for recording timestamped metrics and querying them from a small command-line interface. It is being developed in stages to explore the building blocks behind a time-series database.

> Current version: a single-process prototype with an in-memory collection and JSON file persistence. The SQL-like query language, indexing, and compressed binary storage are planned work, not implemented features.

## What it can do today

- Record a named numeric metric, such as `cpu = 23.5`.
- Attach the current timestamp to each measurement.
- List measurements loaded for the current run.
- Calculate an average with `avg cpu` (metric names are case-insensitive).
- Save after each new measurement and reload from a local JSON file on startup.
- Accept decimal numbers written with either a dot or a comma.

## See it in action

```text
ChronoDB — tiny time-series database
Loaded 0 measurement(s).
Commands: add, list, avg <metric>, help, quit

> add
Metric name (for example, cpu): cpu
Value (for example, 23.5): 23.5
Added and saved cpu = 23.5.

> avg cpu
Average cpu = 23.5

> quit
Goodbye!
```

## Get started

### Requirements

- .NET 10 SDK

### Run from the project folder

```powershell
dotnet run
```

### Run from the parent folder

```powershell
dotnet run --project ChronoDB\ChronoDB.csproj
```

When the app is running, enter `help` to see the commands. Type `quit` to close it. Measurements are stored in `measurements.json` beside the project file. This file is local and excluded from Git.

## Commands

| Command | Description |
| --- | --- |
| `add` | Prompts for a metric name and value, records the current time, then saves it. |
| `list` | Displays all measurements currently loaded from the JSON file. |
| `avg cpu` | Calculates the average of measurements named `cpu`. |
| `help` | Displays the command list. |
| `quit` or `exit` | Closes ChronoDB. |

## How it is organized

```text
ChronoDB/
├── Cli/                 # Interactive command prompt and command handling
├── Models/              # Measurement data type
├── Storage/             # JSON load/save implementation
├── docs/                # Architecture notes and development roadmap
├── .github/workflows/   # Automated build on pushes and pull requests
├── Program.cs           # Application entry point and object setup
├── ChronoDB.csproj      # .NET project configuration
└── README.md
```

The storage interface is intentionally small. This keeps the command-line layer separate from the persistence implementation and leaves room to add a custom storage engine later.

## Build

```powershell
dotnet build --configuration Release
```

GitHub Actions builds the project automatically for pushes and pull requests.

## Current design limits

- Data is stored in a readable JSON file and loaded into memory at startup.
- Adding a measurement rewrites the JSON file, so this is intended for learning and small examples.
- There is no SQL parser, time-range filtering, custom index, binary log, compression, concurrency control, or production durability yet.

See [the architecture notes](docs/ARCHITECTURE.md) for the current data flow and [the roadmap](docs/ROADMAP.md) for planned learning milestones.

## License

ChronoDB is provided for portfolio viewing. All rights are reserved. GitHub users may view and fork this public repository as allowed by [GitHub's Terms of Service](https://docs.github.com/en/site-policy/github-terms/github-terms-of-service). No other permission to reuse the code is granted without written permission. See [LICENSE](LICENSE).

Because the repository is public, people can view and fork it on GitHub. This notice reserves your rights outside GitHub's required public-repository permissions; it cannot technically prevent someone from copying code.
