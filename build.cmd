@echo off
setlocal
cd /d "%~dp0"
where dotnet >nul 2>nul
if errorlevel 1 (
  echo.
  echo ERROR: .NET 10 SDK was not found in PATH.
  echo Install .NET 10 SDK, reopen this folder, then run build.cmd again.
  echo.
  pause
  exit /b 1
)
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1"
if errorlevel 1 (
  echo.
  echo Build failed. Review the messages above.
  pause
  exit /b 1
)
echo.
echo Build completed successfully.
echo Output: %~dp0dist\Jellyfin.Plugin.ElCinema_0.3.0.zip
pause
