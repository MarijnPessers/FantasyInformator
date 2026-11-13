# SessionStart hook: tells Claude which work-in-progress files wait in specs/WIP, with the first line of each.
# Prints nothing when there are none.

$ErrorActionPreference = 'Stop'
[Console]::OutputEncoding = [Text.Encoding]::UTF8

$wipFolder = Join-Path ([IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))) 'specs\WIP'
if (-not (Test-Path -LiteralPath $wipFolder)) { exit 0 }
$files = Get-ChildItem -LiteralPath $wipFolder -File |
    Where-Object { $_.Name -match '^WIP(_\d+)?\.spec\.md$' } |
    Sort-Object { if ($_.Name -match '_(\d+)\.') { [int] $Matches[1] } else { 0 } }
if (-not $files) { exit 0 }

$lines = foreach ($file in $files) {
    $firstLine = Get-Content -LiteralPath $file.FullName -TotalCount 1 -Encoding UTF8
    "- specs/WIP/$($file.Name): $firstLine"
}
$context = "Work-in-progress files wait in specs/WIP. Follow the WIP rule in CLAUDE.local.md.`n" + ($lines -join "`n")
@{
    hookSpecificOutput = @{
        hookEventName     = 'SessionStart'
        additionalContext = $context
    }
} | ConvertTo-Json -Depth 3 -Compress
exit 0
