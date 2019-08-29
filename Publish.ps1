# Project Output Paths
$modOutputPath = "Heroes.Fun.AuraColorizer/bin"
$solutionName = "Heroes.Fun.AuraColorizer.sln"
$publishName = "Heroes.Fun.AuraColorizer.zip"
$publishDirectory = "Publish"

if ([System.IO.Directory]::Exists($publishDirectory)) {
	Get-ChildItem $publishDirectory -Include * -Recurse | Remove-Item -Force -Recurse
}

# Build
dotnet restore $solutionName
dotnet clean $solutionName
dotnet build -c Release $solutionName

# Cleanup
Get-ChildItem $modOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $modOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

# Make compressed directory
if (![System.IO.Directory]::Exists($publishDirectory)) {
    New-Item $publishDirectory -ItemType Directory
}

# Compress
Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory( $modOutputPath + '/Release', $publishDirectory + '/' + $publishName)