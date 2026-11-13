# PreToolUse hook: asks the user before a tool call that touches a path outside this solution.
# Reads the tool call as JSON from stdin. Prints an ask decision when a path is outside.
# Inside the solution it only puts the tool icon in front of a shell command's description.
# Shell commands are checked on the paths written in them, so a program that reaches outside on its own is not caught.

$ErrorActionPreference = 'Stop'
[Console]::InputEncoding = [Text.Encoding]::UTF8
[Console]::OutputEncoding = [Text.Encoding]::UTF8

$root = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))
# Claude's own folder for this project (memory and saved tool output).
$claudeProjectDir = Join-Path $env:USERPROFILE ('.claude\projects\' + ($root -replace '[^A-Za-z0-9]', '-'))
$allowedRoots = @($root, $claudeProjectDir)

$hookInput = [Console]::In.ReadToEnd() | ConvertFrom-Json
$toolInput = $hookInput.tool_input
$cwd = if ($hookInput.cwd) { $hookInput.cwd } else { $root }

# Shown in front of the prompts, so the user sees which tool asks. U+FE0F makes the symbol show as an emoji.
$emojiStyle = [string] [char] 0xFE0F
$toolIcons = @{
    PowerShell = [char]::ConvertFromUtf32(0x1F5A5) + $emojiStyle
    Bash       = [char]::ConvertFromUtf32(0x1F4B2)
    Read       = [char]::ConvertFromUtf32(0x1F4D6)
    Grep       = [char]::ConvertFromUtf32(0x1F50D)
    Glob       = [char]::ConvertFromUtf32(0x1F5C2) + $emojiStyle
    Edit       = [char]::ConvertFromUtf32(0x270F) + $emojiStyle
    Write      = [char]::ConvertFromUtf32(0x270F) + $emojiStyle
}
$toolIcon = $toolIcons[[string] $hookInput.tool_name]
$iconPrefix = if ($toolIcon) { "$toolIcon " } else { '' }

function Write-Ask([string] $reason) {
    $noEntry = [char]::ConvertFromUtf32(0x26D4)
    $roadwork = [char]::ConvertFromUtf32(0x1F6A7)
    $label = "$iconPrefix$noEntry$roadwork OUTSIDE SOLUTION $roadwork$noEntry"
    $message = "$label $reason (solution: $root)"
    $output = @{
        systemMessage      = $message
        hookSpecificOutput = @{
            hookEventName            = 'PreToolUse'
            permissionDecision       = 'ask'
            permissionDecisionReason = $message
        }
    }
    # Not every prompt shows the reason, so the tool call's own description gets the icon too.
    if ($toolInput.PSObject.Properties['description']) {
        $toolInput.description = "$label $($toolInput.description)"
        $output.hookSpecificOutput.updatedInput = $toolInput
    }
    $output | ConvertTo-Json -Depth 5 -Compress
    exit 0
}

# Inside the solution: no decision, only the tool icon in front of the description, so a normal prompt shows it too.
function Write-Icon {
    if ($toolIcon -and $toolInput.PSObject.Properties['description']) {
        $toolInput.description = "$iconPrefix$($toolInput.description)"
        @{
            hookSpecificOutput = @{
                hookEventName = 'PreToolUse'
                updatedInput  = $toolInput
            }
        } | ConvertTo-Json -Depth 5 -Compress
    }
    exit 0
}

function Test-Inside([string] $path, [string] $basePath) {
    if ($path -eq '/dev/null') { return $true }
    # Git Bash drive paths: /d/Code -> d:/Code
    $path = $path -replace '^/([A-Za-z])(?:/|$)', '$1:/'
    # Only the part before the first wildcard is a real path.
    $path = ($path -split '[*?\[{]', 2)[0]
    try {
        $full = [IO.Path]::GetFullPath([IO.Path]::Combine($basePath, $path)).TrimEnd('\')
    }
    catch {
        return $false
    }
    foreach ($allowed in $allowedRoots) {
        if ($full -eq $allowed -or $full.StartsWith($allowed + '\', [StringComparison]::OrdinalIgnoreCase)) {
            return $true
        }
    }
    return $false
}

if ($hookInput.tool_name -in 'Bash', 'PowerShell') {
    $command = [string] $toolInput.command

    # References to folders outside the solution that cannot be resolved here.
    $outsideReferences = @(
        '\$env:(USERPROFILE|HOME|HOMEDRIVE|HOMEPATH|APPDATA|LOCALAPPDATA|TEMP|TMP|ProgramData|ProgramFiles|ProgramW6432|SystemRoot|windir|PUBLIC|OneDrive)\b',
        '\$(HOME|TMPDIR|TEMP|TMP|USERPROFILE|APPDATA|LOCALAPPDATA|PROFILE|PSHOME)\b',
        '%(USERPROFILE|HOMEPATH|APPDATA|LOCALAPPDATA|TEMP|TMP|ProgramData|ProgramFiles|SystemRoot|windir)%',
        '(?<![\w$])~(?=[\\/\s''"]|$)',
        '\b(HKLM|HKCU|HKCR|HKU|HKCC):',
        '\bRegistry::',
        '(?<![\w])\\\\[^\s''"`;|&<>(){},\\]'
    )
    foreach ($pattern in $outsideReferences) {
        $match = [regex]::Match($command, $pattern, 'IgnoreCase')
        if ($match.Success) { Write-Ask "'$($match.Value)'" }
    }

    $stop = '[^\s''"`;|&<>(){},]*'
    $pathPatterns = @(
        "(?<![\w.:/\\])[A-Za-z]:[\\/]$stop",   # D:\x or D:/x
        "(?<![\w.:/\\~*-])/[^\s'`"``;|&<>(){},/]$stop",   # /c/x, /tmp/x, but not the /x in a glob such as **/x
        "(?<![\w.:/\\])\.\.(?:[\\/]$stop)?"    # .., ..\x, ../x
    )
    foreach ($pattern in $pathPatterns) {
        foreach ($match in [regex]::Matches($command, $pattern)) {
            if (-not (Test-Inside $match.Value $cwd)) { Write-Ask "'$($match.Value)'" }
        }
    }
    Write-Icon
}

$basePath = if ($toolInput.path) { [IO.Path]::Combine($cwd, $toolInput.path) } else { $cwd }
$fields = switch ($hookInput.tool_name) {
    'Glob' { 'path', 'pattern' }
    'Grep' { 'path', 'glob' }
    default { 'file_path', 'notebook_path', 'path' }
}
foreach ($field in $fields) {
    $value = [string] $toolInput.$field
    if (-not $value) { continue }
    $base = if ($field -eq 'path') { $cwd } else { $basePath }
    if (-not (Test-Inside $value $base)) { Write-Ask "'$value'" }
}
exit 0
