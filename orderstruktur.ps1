# Datei: create_structure.ps1

$dirs = @(
    "Controllers",
    "Services",
    "Repositories",
    "Data",
    "Models",
    "DTOs",
    "Middleware"
)

Write-Host " Erstelle Projektordner im aktuellen Verzeichnis ..."

foreach ($dir in $dirs) {
    New-Item -ItemType Directory -Path $dir -Force | Out-Null
    Write-Host " $dir erstellt"
}

Write-Host " Fertig! Ordnerstruktur erstellt."