using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.RepositoryBootstrap.Configuration;

public sealed class PluginConfiguration : BasePluginConfiguration
{
    public string RepositoryName { get; set; } = "Rafat Jellyfin Plugins";

    public string RepositoryUrl { get; set; } =
        "https://raw.githubusercontent.com/rqwe111111/Jellyfin.Plugin.ElCinema/main/manifest.json";

    public bool RepositoryEnabled { get; set; } = true;

    public bool AddOnStartup { get; set; } = true;

    public bool UpdateExisting { get; set; } = true;
}
