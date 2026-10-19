# Roadmap

ChronoDB is an educational portfolio project. These milestones are goals, not claims about the current implementation.

- [x] Record timestamped numeric measurements
- [x] List measurements and calculate an average by metric name
- [x] Persist and reload data using JSON
- [ ] Add minimum, maximum, and count aggregate queries
- [ ] Filter metrics by a time range
- [ ] Split command parsing from query execution
- [ ] Design a SQL-like query grammar and lexer
- [ ] Parse queries into a small syntax tree
- [ ] Add a ring buffer for bounded in-memory ingestion
- [ ] Explore time-bucketed indexing and compare query costs
- [ ] Replace JSON persistence with an append-only binary log
- [ ] Explore timestamp delta-of-delta encoding and benchmark storage size
- [ ] Add automated tests for parsing, aggregation, storage, and edge cases

The order may change as the project develops. Keep the README's implemented-feature list aligned with working code.
