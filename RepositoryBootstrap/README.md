# Jellyfin Repository Bootstrap

A small Jellyfin 12.1 plugin that adds or updates a plugin repository automatically.

## What it does

On Jellyfin startup it reads the plugin settings and synchronizes one repository into:

Dashboard -> Plugins -> Repositories

It never removes unrelated repositories.

Matching is done by URL first, then by repository name, so duplicate entries are avoided.

## Default repository

- Name: Rafat Jellyfin Plugins
- Manifest URL:
  https://raw.githubusercontent.com/rqwe111111/Jellyfin.Plugin.ElCinema/main/manifest.json

You can change both values from the plugin settings page.

## Build

Requires .NET 10 SDK.

dotnet publish Jellyfin.Plugin.RepositoryBootstrap.csproj -c Release -o publish

The GitHub Actions workflow builds a ZIP automatically.
