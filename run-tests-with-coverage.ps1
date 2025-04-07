Write-Host "`nEjecutando tests con cobertura..." -ForegroundColor Cyan

# Buscar el archivo .sln de forma recursiva
$solutionFile = Get-ChildItem -Path $PSScriptRoot -Filter "*.sln" -Recurse | Select-Object -First 1

if (-not $solutionFile) {
    Write-Error "❌ No se encontró ningún archivo .sln."
    exit 1
}

$solutionPath = $solutionFile.FullName
$coverageSettings = Join-Path $PSScriptRoot "tests\coverage.runsettings"
$reportDir = Join-Path $PSScriptRoot "tests\FS.FakeTwiter.IntegrationTests\coverage-report"

# Ejecutar tests con cobertura
dotnet test $solutionPath `
  --collect:"XPlat Code Coverage" `
  --settings $coverageSettings `
  --results-directory $reportDir `
  --no-build

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Error al ejecutar los tests."
    exit $LASTEXITCODE
}

# Generar reporte HTML
try {
    reportgenerator `
      -reports:"tests/**/coverage-report/**/coverage.cobertura.xml" `
      -targetdir:"tests/coverage-report" `
      -reporttypes:HtmlInline_AzurePipelines
  } catch {
    Write-Host "⚠️ No se pudo generar el reporte HTML. ¿Instalaste dotnet-reportgenerator-globaltool?"
}
Write-Host "`n✅ Tests ejecutados con éxito. Reporte generado en:"
Write-Host "$reportDir\index.htm" -ForegroundColor Green
Start-Process "tests\coverage-report\index.htm"
