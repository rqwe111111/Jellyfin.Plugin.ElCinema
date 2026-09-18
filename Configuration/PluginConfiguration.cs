using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.ElCinema.Configuration;

public class PluginConfiguration : BasePluginConfiguration
{
    public bool EnableProvider { get; set; } = true;
    public bool EnableMovieMetadata { get; set; } = true;
    public bool EnableSeriesMetadata { get; set; } = true;
    public bool EnableSeasonMetadata { get; set; } = true;
    public bool EnableEpisodeMetadata { get; set; } = true;
    public bool EnablePeopleMetadata { get; set; } = true;
    public bool EnableImages { get; set; } = true;
    public bool EnableGalleryImages { get; set; } = true;
    public bool PreferArabicTitles { get; set; } = true;
    public bool EnableCast { get; set; } = true;
    public bool EnableCrew { get; set; } = true;
    public int CacheHours { get; set; } = 24;
    public int RequestDelayMilliseconds { get; set; } = 900;
    public int RequestTimeoutSeconds { get; set; } = 30;
    public int RetryCount { get; set; } = 2;
    public int MaxSearchResults { get; set; } = 20;
    public int MaxCastMembers { get; set; } = 40;
    public int MaxImages { get; set; } = 10;
    public string BaseUrl { get; set; } = Constants.DefaultBaseUrl;
    public string UserAgent { get; set; } = "Mozilla/5.0 (compatible; Jellyfin-elCinema/0.3; +https://jellyfin.org/)";
}
