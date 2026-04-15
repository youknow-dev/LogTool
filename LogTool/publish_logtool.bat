@echo off
setlocal

set PROJECT=LogTool.csproj

echo Publishing win-x64...
dotnet publish "%PROJECT%" -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o bin\publish\win-x64
if errorlevel 1 goto :fail

echo Publishing win-arm64...
dotnet publish "%PROJECT%" -c Release -r win-arm64 --self-contained true /p:PublishSingleFile=true -o bin\publish\win-arm64
if errorlevel 1 goto :fail

echo Publishing linux-x64...
dotnet publish "%PROJECT%" -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true -o bin\publish\linux-x64
if errorlevel 1 goto :fail

echo Publishing linux-arm64...
dotnet publish "%PROJECT%" -c Release -r linux-arm64 --self-contained true /p:PublishSingleFile=true -o bin\publish\linux-arm64
if errorlevel 1 goto :fail

echo Publishing osx-x64...
dotnet publish "%PROJECT%" -c Release -r osx-x64 --self-contained true /p:PublishSingleFile=true -o bin\publish\osx-x64
if errorlevel 1 goto :fail

echo Publishing osx-arm64...
dotnet publish "%PROJECT%" -c Release -r osx-arm64 --self-contained true /p:PublishSingleFile=true -o bin\publish\osx-arm64
if errorlevel 1 goto :fail

echo.
echo All publishes completed successfully.
goto :end

:fail
echo.
echo Publish failed.
exit /b 1

:end
endlocal
pause