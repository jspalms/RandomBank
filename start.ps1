echo "====== Docker Compose Startup Script ======"

# Check if Docker is running
echo "Checking Docker status..."
$dockerStatus = docker info 2>&1
if ($LASTEXITCODE -ne 0) {
    echo "ERROR: Docker is not running or not installed properly. Please start Docker Desktop first."
    exit 1
} else {
    echo "Docker is running."
}

# Check Docker Compose version
echo "Docker Compose version:"
docker compose version

# Verify the docker-compose.yml file exists
if (-not (Test-Path .\docker-compose.yml)) {
    echo "ERROR: docker-compose.yml file not found in the current directory."
    echo "Current directory: $(Get-Location)"
    exit 1
} else {
    echo "docker-compose.yml found at: $(Get-Item .\docker-compose.yml | Select-Object FullName)"
    echo "File content:"
    Get-Content .\docker-compose.yml | ForEach-Object { echo "  $_" }
}

# Check for proper YAML formatting
echo "Validating docker-compose.yml..."
try {
    docker compose --file .\docker-compose.yml config > $null 2>&1
    if ($LASTEXITCODE -ne 0) {
        echo "ERROR: docker-compose.yml has syntax errors. Please check the file format."
        exit 1
    } else {
        echo "docker-compose.yml validation passed."
    }
} catch {
    echo "ERROR: Failed to validate docker-compose.yml: $_"
    exit 1
}

# Check for required script files
echo "Checking for required script files..."
$scriptsOk = $true
if (-not (Test-Path .\kafka_init.sh)) {
    echo "WARNING: kafka_init.sh is missing. Creating it now..."
    @"
#!/bin/bash
echo "Waiting for Kafka broker to be ready..."
sleep 20
echo "Creating Kafka topics..."
kafka-topics --bootstrap-server broker:29092 --create --if-not-exists --topic account-created --partitions 3 --replication-factor 1
kafka-topics --bootstrap-server broker:29092 --create --if-not-exists --topic transaction-created --partitions 3 --replication-factor 1
echo "Kafka topics created successfully!"
"@ | Out-File -FilePath .\kafka_init.sh -Encoding utf8
    echo "Created kafka_init.sh"
}

if (-not (Test-Path .\keycloak_setup.sh)) {
    echo "WARNING: keycloak_setup.sh is missing. Creating it now..."
    @"
#!/bin/bash
echo "Keycloak setup script starting..."
echo "Waiting for Keycloak to be ready..."
# Add your Keycloak configuration here if needed
echo "Keycloak setup completed successfully!"
"@ | Out-File -FilePath .\keycloak_setup.sh -Encoding utf8
    echo "Created keycloak_setup.sh"
}

# Make scripts executable if Git bash is available
if (Test-Path "C:\Program Files\Git\bin\bash.exe") {
    echo "Setting executable permissions on scripts using Git Bash..."
    & "C:\Program Files\Git\bin\bash.exe" -c "chmod +x ./kafka_init.sh ./keycloak_setup.sh"
    echo "Permissions set successfully."
} else {
    echo "WARNING: Git Bash not found. Script files might not be executable in containers."
}

# Check for ports in use
echo "Checking for port conflicts..."
$usedPorts = @(5432, 9092, 8081, 8080)
$conflictingPorts = @()

foreach ($port in $usedPorts) {
    $portCheck = netstat -ano | findstr ":$port "
    if ($portCheck) {
        $conflictingPorts += $port
    }
}

if ($conflictingPorts.Count -gt 0) {
    echo "WARNING: The following ports are already in use: $($conflictingPorts -join ', ')"
    echo "This might prevent some containers from starting."
    echo "Press Ctrl+C to abort, or any key to continue anyway..."
    $null = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
}

echo "Starting Docker containers in verbose mode..."
echo "This might take a while for the first time. Please be patient..."
# Run without detaching to see live output
docker compose --file .\docker-compose.yml up --build -d
