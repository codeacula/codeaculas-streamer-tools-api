Write-Host "🧪 Running Unit & Integration Tests..."

# Ensure test-results directory exists
$coverageDir = "test-results"
if (!(Test-Path $coverageDir)) { New-Item -ItemType Directory -Path $coverageDir }

# 1️⃣ Run Unit Tests Locally (Automatically Builds & Collects Coverage)
Write-Host "🔍 Running Unit Tests..."
dotnet test --settings tests/test.runsettings

# 2️⃣ Run Integration Tests in Docker (Without Coverage)
Write-Host "🔌 Running Integration Tests in Docker..."
docker-compose -f config/docker-compose.integration.yml up --build

# 3️⃣ Generate VS Code Coverage Reports (If Needed)
if (Test-Path "$coverageDir/**/coverage.cobertura.xml") {
    Write-Host "📊 Generating VS Code-friendly coverage report..."
    reportgenerator -reports:$coverageDir/**/coverage.cobertura.xml -targetdir $coverageDir/report -reporttypes:lcov
    Write-Host "✅ Coverage updated! Open test-results/report/index.html"
} else {
    Write-Host "⚠️ Warning: No coverage report found! Check your test.runsettings file."
}
