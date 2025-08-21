
$csproj = './WorkerService1.csproj'
$exe = './bin/Release/net9.0/publish/WorkerService1.exe'

# dotnet build $csproj --no-logo --verbosity quiet -c Release
dotnet publish $csproj -c Release -v quiet -nologo


# Run the application, without setting the environment
& $exe --FastExit=true


$environments = @(
      "Development"
    , "Staging"
    , "Production"
    , "TSCT+FV-C801-PLC0001"
    )

foreach ($e in $environments) {
    Write-Host "Running with Environment '$e' ..."

    # # Using the environment variable DOTNET_ENVIRONMENT
    # $env:DOTNET_ENVIRONMENT = $e
    # & $exe

    # Using CLI Arguments
    & $exe --environment $e --FastExit=true

    # Powershell 7.2+
    # Start-Process -FilePath $exe -Wait -NoNewWindow -Environment @{ DOTNET_ENVIRONMENT = $e }
}

# Clean up the environment variable after running
# This prevents next run to start with the last environment
Remove-Item Env:DOTNET_ENVIRONMENT -ErrorAction SilentlyContinue