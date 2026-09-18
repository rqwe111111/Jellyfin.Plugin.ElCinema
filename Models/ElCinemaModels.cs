using Jellyfin.Data.Enums;

namespace Jellyfin.Plugin.ElCinema.Models;

public enum ElCinemaWorkKind { Unknown, Movie, Series }

public sealed class ElCinemaSearchItem
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int? Year { get; init; }
    public string? ImageUrl { get; init; }
    public ElCinemaWorkKind Kind { get; init; }
}

public sealed class ElCinemaPersonCredit
{
    public string Name { get; init; } = string.Empty;
    public string? Role { get; init; }
    public string? Id { get; init; }
    public PersonKind Kind { get; init; }
}

public sealed class ElCinemaWork
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? OriginalTitle { get; set; }
    public string? Overview { get; set; }
    public int? Year { get; set; }
    public DateTime? PremiereDate { get; set; }
    public int? RuntimeMinutes { get; set; }
    public float? Rating { get; set; }
    public string? PosterUrl { get; set; }
    public string? Language { get; set; }
    public string? OfficialRating { get; set; }
    public ElCinemaWorkKind Kind { get; set; }
    public List<string> Genres { get; } = [];
    public List<string> ProductionLocations { get; } = [];
    public List<ElCinemaPersonCredit> People { get; } = [];
}

public sealed class ElCinemaEpisodeListItem
{
    public string WorkId { get; init; } = string.Empty;
    public string EpisodeId { get; init; } = string.Empty;
    public int SeasonNumber { get; init; } = 1;
    public int EpisodeNumber { get; init; }
    public string? Title { get; init; }
}

public sealed class ElCinemaEpisode
{
    public string WorkId { get; init; } = string.Empty;
    public string EpisodeId { get; init; } = string.Empty;
    public int SeasonNumber { get; set; } = 1;
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Overview { get; set; }
    public DateTime? PremiereDate { get; set; }
    public int? Year { get; set; }
    public int? RuntimeMinutes { get; set; }
    public float? Rating { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> Genres { get; } = [];
    public List<ElCinemaPersonCredit> People { get; } = [];
}

public sealed class ElCinemaPersonDetails
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? OriginalName { get; set; }
    public string? Overview { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? BirthYear { get; set; }
    public string? Country { get; set; }
    public string? ImageUrl { get; set; }
}
