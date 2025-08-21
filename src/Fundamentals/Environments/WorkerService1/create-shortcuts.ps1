function New-WorkerServiceShortcut {
    param(
        [Parameter(Mandatory=$true)]
        [string]$EnvironmentName,
        [string]$ExePath = "./bin/Release/net9.0/publish/WorkerService1.exe",
        [string]$ShortcutDir = "./shortcuts"
    )

    # Ensure dirs exist
    if (-not (Test-Path $ShortcutDir)) { New-Item -ItemType Directory -Path $ShortcutDir -Force | Out-Null }

    # Resolve to full paths (throws if not found)
    try {
        $exeFull = (Resolve-Path -Path $ExePath).Path
    } catch {
        throw "Exe not found: $ExePath"
    }
    $workDir = Split-Path -Path $exeFull -Parent
    $lnkFull = Join-Path (Resolve-Path $ShortcutDir).Path "WorkerService1.$EnvironmentName.lnk"

    # Create shortcut
    $wsh = New-Object -ComObject WScript.Shell
    $sc  = $wsh.CreateShortcut($lnkFull)
    $sc.TargetPath = $exeFull                      # MUST be absolute
    $sc.Arguments  = "--environment `"$EnvironmentName`" --FastExit=False"
    $sc.WorkingDirectory = $workDir
    $sc.IconLocation = "$exeFull,0"
    $sc.Save()

    "Shortcut created: $lnkFull"
}

# Examples
New-WorkerServiceShortcut -EnvironmentName "Development"
New-WorkerServiceShortcut -EnvironmentName "Staging"
New-WorkerServiceShortcut -EnvironmentName "Production"
New-WorkerServiceShortcut -EnvironmentName "TSCT+FV-C801-PLC0001"
