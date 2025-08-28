# Script to update ViewModels to use the new MVVM infrastructure

# Get all ViewModel files
$viewModels = Get-ChildItem -Path "$PSScriptRoot\..\ViewModels\*.cs" -Recurse | 
    Where-Object { $_.Name -like "*ViewModel.cs" -and $_.FullName -notlike '*\obj\*' -and $_.FullName -notlike '*\bin\*' }

Write-Host "Found $($viewModels.Count) ViewModel files to process..." -ForegroundColor Cyan

foreach ($file in $viewModels) {
    $content = Get-Content -Path $file.FullName -Raw
    $originalContent = $content
    $changesMade = $false
    
    Write-Host "Processing $($file.Name)..." -ForegroundColor Yellow
    
    # 1. Remove MvvmLight usings
    $content = $content -replace 'using GalaSoft\.MvvmLight(?:\.[A-Za-z]+)*;\r?\n', ''
    
    # 2. Add our infrastructure using if not present
    if (-not ($content -match 'using AstralBattles\.Core\.Infrastructure;')) {
        $content = $content -replace '(namespace|public class)', 'using AstralBattles.Core.Infrastructure;

$1'
        $changesMade = $true
    }
    
    # 3. Update RelayCommand usage
    $content = $content -replace 'new RelayCommand\(\s*new Action<[^>]+>\s*\(([^)]+)\)', 'new RelayCommand($1'
    $content = $content -replace 'new RelayCommand\(\s*new Action\s*\(([^)]+)\)', 'new RelayCommand($1'
    $content = $content -replace 'new RelayCommand\(_ => ([^,)]+)', 'new RelayCommand(_ => $1'
    
    # 4. Update property changed notifications
    $content = $content -replace 'this\.RaisePropertyChanged\(([^)]+)\)', 'RaisePropertyChanged($1)'
    $content = $content -replace 'this\.Set\(([^,]+),\s*([^)]+)\)', 'SetProperty(ref $1, $2)'
    $content = $content -replace 'this\.', ''
    
    # Save the file if changes were made
    if ($content -ne $originalContent) {
        $changesMade = $true
        $content | Set-Content -Path $file.FullName -NoNewline -Encoding UTF8
        Write-Host "  - Updated $($file.Name)" -ForegroundColor Green
    }
    
    if (-not $changesMade) {
        Write-Host "  - No changes needed" -ForegroundColor Gray
    }
}

Write-Host "`nViewModel update complete!" -ForegroundColor Green
