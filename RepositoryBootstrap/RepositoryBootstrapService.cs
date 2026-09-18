using MediaBrowser.Controller.Configuration;
using MediaBrowser.Model.Updates;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.RepositoryBootstrap;

public sealed class RepositoryBootstrapService(
    IServerConfigurationManager serverConfigurationManager,
    ILogger<RepositoryBootstrapService> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var plugin = Plugin.Instance;
        var cfg = plugin?.Configuration;

        if (cfg is null || !cfg.AddOnStartup)
        {
            return Task.CompletedTask;
        }

        var name = cfg.RepositoryName?.Trim();
        var url = cfg.RepositoryUrl?.Trim();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url))
        {
            logger.LogWarning("Repository Bootstrap skipped: repository name or URL is empty.");
            return Task.CompletedTask;
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeHttp))
        {
            logger.LogWarning("Repository Bootstrap skipped: invalid repository URL {Url}", url);
            return Task.CompletedTask;
        }

        var repositories = serverConfigurationManager.Configuration.PluginRepositories.ToList();

        var existing = repositories.FirstOrDefault(r =>
            string.Equals(r.Url, url, StringComparison.OrdinalIgnoreCase))
            ?? repositories.FirstOrDefault(r =>
                string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));

        var changed = false;

        if (existing is null)
        {
            repositories.Add(new RepositoryInfo
            {
                Name = name,
                Url = url,
                Enabled = cfg.RepositoryEnabled
            });

            changed = true;
            logger.LogInformation(
                "Added Jellyfin plugin repository {Name}: {Url}",
                name,
                url);
        }
        else if (cfg.UpdateExisting)
        {
            if (!string.Equals(existing.Name, name, StringComparison.Ordinal)
                || !string.Equals(existing.Url, url, StringComparison.Ordinal)
                || existing.Enabled != cfg.RepositoryEnabled)
            {
                existing.Name = name;
                existing.Url = url;
                existing.Enabled = cfg.RepositoryEnabled;
                changed = true;

                logger.LogInformation(
                    "Updated Jellyfin plugin repository {Name}: {Url}",
                    name,
                    url);
            }
        }

        if (changed)
        {
            serverConfigurationManager.Configuration.PluginRepositories = repositories.ToArray();
            serverConfigurationManager.SaveConfiguration();
        }
        else
        {
            logger.LogInformation(
                "Jellyfin plugin repository {Name} is already configured.",
                name);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
