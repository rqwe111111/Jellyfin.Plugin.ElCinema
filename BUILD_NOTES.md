# Build / test checklist

- Target: Jellyfin 12.1, net10.0, Controller/Model/Data 12.1.0.
- Run `build.cmd` on Windows with .NET 10 SDK.
- If compilation reports an API signature change in Jellyfin 12.1, keep the error text and line number; it is usually a small interface adjustment.
- Test in a disposable Jellyfin library first.
- Suggested test sequence: movie Identify -> series Identify -> season refresh -> one episode refresh -> person refresh -> image refresh -> full series refresh.
- Keep `RequestDelayMilliseconds` around 900ms initially.
