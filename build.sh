#!/usr/bin/env bash
set -euo pipefail
command -v dotnet >/dev/null 2>&1 || { echo ".NET 10 SDK (dotnet) was not found in PATH." >&2; exit 1; }
cd "$(dirname "$0")"
VERSION="0.3.0"
OUT="dist/Jellyfin.Plugin.ElCinema_${VERSION}"
rm -rf publish dist
mkdir -p publish "$OUT"
dotnet restore Jellyfin.Plugin.ElCinema.csproj
dotnet publish Jellyfin.Plugin.ElCinema.csproj -c Release -o publish --no-restore
cp publish/Jellyfin.Plugin.ElCinema.dll "$OUT/"
find publish -maxdepth 1 -type f -name '*.dll' \
  ! -name 'Jellyfin.*' ! -name 'MediaBrowser.*' ! -name 'Microsoft.*' ! -name 'System.*' ! -name 'Jellyfin.Plugin.ElCinema.dll' \
  -exec cp {} "$OUT/" \;
(cd dist && zip -r "Jellyfin.Plugin.ElCinema_${VERSION}.zip" "Jellyfin.Plugin.ElCinema_${VERSION}")
echo "Built dist/Jellyfin.Plugin.ElCinema_${VERSION}.zip"
