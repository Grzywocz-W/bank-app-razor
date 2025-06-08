Write-Host "Building Docker image..."
docker build -t bankapp .

Write-Host "Starting container..."
docker run -d --name bankapp-container -p 8080:80 bankapp

Write-Host "Application running at http://localhost:8080"
