Write-Host "🚀 Starting Production Build..."
docker-compose -f config/docker-compose.prod.yml up --build
