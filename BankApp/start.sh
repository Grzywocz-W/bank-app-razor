#!/bin/bash
echo "Building Docker image..."
docker build -t bankapp .

echo "Starting container..."
docker run -d --name bankapp-container -p 8080:80 bankapp

echo "Application running at http://localhost:8080"
