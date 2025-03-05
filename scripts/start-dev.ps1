Write-Host "Starting Development Environment with Hot Reload..."

docker-compose -f config/docker-compose.override.yml up --build -d
