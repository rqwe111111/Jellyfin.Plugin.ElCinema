$ErrorActionPreference = 'Stop'
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { throw '.NET 10 SDK (dotnet) was not found in PATH.' }
$project = Join-Path $PSScriptRoot 'Jellyfin.Plugin.ElCinema.csproj'
$dist = Join-Path $PSScriptRoot 'dist'
$publish = Join-Path $PSScriptRoot 'publish'
$version = '0.3.0'
$folderName = "Jellyfin.Plugin.ElCinema_$version"
$outFolder = Join-Path $dist $folderName
$zip = Join-Path $dist "$folderName.zip"

if (Test-Path $publish) { Remove-Item $publish -Recurse -Force }
if (Test-Path $outFolder) { Remove-Item $outFolder -Recurse -Force }
if (Test-Path $zip) { Remove-Item $zip -Force }
New-Item -ItemType Directory -Force -Path $publish, $outFolder | Out-Null

dotnet restore $project
dotnet publish $project -c Release -o $publish --no-restore

Copy-Item (Join-Path $publish 'Jellyfin.Plugin.ElCinema.dll') $outFolder
Get-ChildItem $publish -Filter '*.dll' | Where-Object {
    $_.Name -notlike 'Jellyfin.*' -and
    $_.Name -notlike 'MediaBrowser.*' -and
    $_.Name -notlike 'Microsoft.*' -and
    $_.Name -notlike 'System.*' -and
    $_.Name -ne 'Jellyfin.Plugin.ElCinema.dll'
} | Copy-Item -Destination $outFolder

Compress-Archive -Path (Join-Path $outFolder '*') -DestinationPath $zip -Force
Write-Host "Built: $zip"
