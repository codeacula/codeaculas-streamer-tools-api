Write-Host "🛠 Checking EF Core Migrations..."

# Get the current Git branch name
$branchName = git rev-parse --abbrev-ref HEAD

# Extract the last part of the branch name (e.g., "44-doing-the-stuff" from "tasks/44-doing-the-stuff")
if ($branchName -match ".*/(?<migrationName>.+)") {
  $migrationName = $matches["migrationName"]
}
else {
  Write-Host "❌ Could not extract a migration name from the branch. Using default."
  $migrationName = "unnamed-migration"
}

Write-Host "🔍 Checking for existing migrations..."
$existingMigrations = dotnet ef migrations list

# 1️⃣ Generate an idempotent SQL script (ensures only necessary changes are applied)
dotnet ef migrations script --idempotent --output migrations-check.sql
Write-Host "✅ Migration script 'migrations-check.sql' generated."

# 2️⃣ If migration already exists, create a new one instead of overwriting
if ($existingMigrations -match $migrationName) {
  Write-Host "⚠️ Migration '$migrationName' already exists. Adding a new incremental migration..."
  dotnet ef migrations add "$migrationName-$(Get-Date -Format 'yyyyMMddHHmmss')"
}
else {
  Write-Host "🆕 Creating new migration: $migrationName"
  dotnet ef migrations add "$migrationName"
}

Write-Host "✅ Migrations are up to date!"
