# Define the solution file and selected project names
$solutionFile = ".\Source\FluentMetadata.sln"
$selectedProjectNames = @("FluentMetadata.Core", "FluentMetadata.AutoMapper", "FluentMetadata.MVC", "FluentMetadata.FluentNHibernate", "FluentMetadata.EntityFramework")

# Build the solution in Release configuration
dotnet build $solutionFile --configuration Release

# Create a release folder relative to the script
$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
$releaseFolder = Join-Path -Path $scriptDirectory -ChildPath "release"

# Clear the release folder
if (Test-Path $releaseFolder) {
    Remove-Item -Path $releaseFolder -Recurse -Force
}

# Create a new release folder
New-Item -Path $releaseFolder -ItemType Directory | Out-Null

# Get the directory of the solution file
$solutionDirectory = Split-Path -Path $solutionFile -Parent

# Function to extract AssemblyName from project file
function Get-AssemblyName {
    param([string]$projectFilePath)

    $projectContent = Get-Content -Path $projectFilePath -Raw
    $assemblyName = $projectContent | Select-String -Pattern '(?<=<AssemblyName>).*?(?=</AssemblyName>)' | ForEach-Object { $_.Matches.Value.Trim() }
    
    return $assemblyName
}

# Iterate over the selected project names
foreach ($projectName in $selectedProjectNames) {
    # Find the project file within the solution content
    $projectPath = dotnet sln $solutionFile list | Where-Object { $_ -match "$projectName.$projectName.csproj" }
    
    if ($projectPath) {
        # Prepend the solution directory to the project path
        $projectPath = Join-Path -Path $solutionDirectory -ChildPath $projectPath

        # Get the assembly name from the project file
        $assemblyName = Get-AssemblyName -projectFilePath $projectPath
        
        # Get the project directory
        $projectDirectory = (Get-Item $projectPath).DirectoryName
        
        # Build output directory path
        $outputDirectory = Join-Path -Path $projectDirectory -ChildPath "bin\Release"

        # Copy .dll, .pdb, and .xml files to the release folder
        foreach ($extension in @(".dll", ".pdb", ".xml")) {
            $sourceFile = Join-Path -Path $outputDirectory -ChildPath "$assemblyName$extension"

            if (Test-Path $sourceFile) {
                $destinationFile = Join-Path -Path $releaseFolder -ChildPath "$assemblyName$extension"
                Copy-Item -Path $sourceFile -Destination $destinationFile -Force
            }
        }
    }
    else {
        Write-Host "Project '$projectName' not found in the solution."
    }
}

Write-Host "Released:"

# Display the list of output files copied to the release folder
Get-ChildItem -Path $releaseFolder | Select-Object -ExpandProperty FullName
