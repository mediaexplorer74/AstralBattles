# Script to backup ViewModel files before making changes

$backupDir = "$PSScriptRoot\..\..\Backup\ViewModels_$(Get-Date -Format 'yyyyMMdd_HHmmss')"
$sourceDir = "$PSScriptRoot\..\ViewModels"

# Create backup directory if it doesn't exist
if (-not (Test-Path -Path $backupDir)) {
    New-Item -ItemType Directory -Path $backupDir -Force | Out-Null
}

# Copy all ViewModel files to backup directory
Get-ChildItem -Path $sourceDir -Filter "*ViewModel.cs" -Recurse | ForEach-Object {
    $relativePath = $_.FullName.Substring($sourceDir.Length + 1)
    $targetPath = Join-Path -Path $backupDir -ChildPath $relativePath
    $targetDir = [System.IO.Path]::GetDirectoryName($targetPath)
    
    if (-not (Test-Path -Path $targetDir)) {
        New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
    }
    
    Copy-Item -Path $_.FullName -Destination $targetPath -Force
}

Write-Host "Backup created at: $backupDir" -ForegroundColor Green
