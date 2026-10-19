# Contributing

Thanks for taking an interest in ChronoDB. The project is intentionally small and focused on learning database fundamentals.

## Before proposing a change

- Check the README and roadmap to see what exists and what is planned.
- Keep claims about implemented features aligned with the working code.
- Prefer small changes that are easy to explain and review.

## Build locally

From the folder containing `ChronoDB.csproj`:

```powershell
dotnet build --configuration Release
```

## Pull requests

Please describe the behavior changed, the reason for the change, and how you checked it. Avoid committing `bin/`, `obj/`, or your local `measurements.json` file.
