# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicheroes.fun.colourizer/*" -Force -Recurse
dotnet publish "./Heroes.Fun.AuraColorizer.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicheroes.fun.colourizer" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location