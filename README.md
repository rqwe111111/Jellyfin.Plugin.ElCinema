# Jellyfin.Plugin.ElCinema Full 0.3.0 (Beta)

Target: **Jellyfin 12.1 / .NET 10**. Plugin ABI: `12.0.0.0`.

## Included
- Movies: Arabic title, original title when available, plot, year/date, runtime, rating, genres, countries, language, content rating, cast/directors/writers, poster.
- Series: same core metadata plus elCinema ID.
- Seasons/parts: season provider using the elCinema series/work ID.
- Episodes: match by series elCinema ID + season/part + episode number, then fetch episode detail page; title, plot, date, runtime, rating, genres, cast/crew and image when available.
- People: search, Arabic/English name, biography, country, birth date/year and image.
- Images: primary poster plus optional gallery images for works and people; episode primary images.
- Identify/manual search support.
- Arabic normalization (diacritics/tatweel/alef variants/Arabic digits), cache, request throttling and retry logic.
- Arabic configuration page.

## Build on Windows
1. Install **.NET 10 SDK**.
2. Extract this source ZIP.
3. Double-click `build.cmd`.
4. Output: `dist/Jellyfin.Plugin.ElCinema_0.3.0.zip`.
5. Stop Jellyfin, extract the built plugin ZIP into a folder under the Jellyfin plugins directory, start Jellyfin again.
6. Enable elCinema in the library metadata downloaders and image fetchers.

## Important
This is a **full-source beta**, not a precompiled/tested DLL. The generation environment used here does not contain .NET 10 SDK, so the project could not be compiled against the actual Jellyfin 12.1 assemblies here. The build script will expose any remaining API-signature changes immediately on your Windows machine.

elCinema pages can change HTML structure. Keep the request delay enabled. For commercial reuse, use licensed data access from elCinema rather than relying on public-page parsing.

## Provider ID format
- Movie/Series/Person: numeric elCinema ID.
- Season: `<workId>:s<seasonNumber>`.
- Episode: `<workId>:e<episodeId>`.
These composite IDs avoid confusing a series ID with an episode ID.
