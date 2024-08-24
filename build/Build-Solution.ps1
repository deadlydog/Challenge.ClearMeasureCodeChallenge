<#
	.SYNOPSIS
	Script to clean, build, test, and publish a .NET Core solution or project.
#>
[CmdletBinding()]
param (
	[Parameter(Mandatory = $false, HelpMessage = 'The directory path of the base of the repository.')]
	[string] $RepositoryBasePath = $PSScriptRoot,

	[Parameter(Mandatory = $false, HelpMessage = 'The path to the solution (.sln) or project (.csproj) files to build. If not provided, the script will attempt to find solution or project files in the repository.')]
	[string[]] $SolutionOrProjectPaths = [string]::Empty,

	[Parameter(Mandatory = $false, HelpMessage = 'The directory path where build artifacts will be published to.')]
	[string] $PublishArtifactsPath = "$RepositoryBasePath/BuildArtifacts",

	[Parameter(Mandatory = $false, HelpMessage = 'The version number to use for the build.')]
	[ValidateScript({ $_ -match '^\d+\.\d+\.\d+(.\d+)?$' })] # Must be in the format of '1.0.0' or '1.0.0.0'.
	[string] $VersionNumber = '1.0.0',

	[Parameter(Mandatory = $false, HelpMessage = 'If provided, the script will not prompt for input before completing.')]
	[switch] $NoPromptBeforeExiting = $false
)

process {
	try {
		if (-not (Test-Path $RepositoryBasePath -PathType Container)) {
			throw "The path '$RepositoryBasePath' does not exist or is not a directory."
		}

		if ([string]::IsNullOrWhiteSpace($SolutionOrProjectPaths)) {
			Write-Information "No solution or project files provided. Attempting to find solution or project files in the repository."
			$SolutionOrProjectPaths = Find-SolutionOrProjectFiles -path $RepositoryBasePath
		}

		Remove-PublishedArtifacts -path $PublishArtifactsPath
		Clean-Solution -path $SolutionOrProjectPaths
		Restore-NugetPackages -path $SolutionOrProjectPaths
		Build-Solution -path $SolutionOrProjectPaths -versionNumber $VersionNumber
		Test-Solution -path $SolutionOrProjectPaths -publishTestResultsPath $PublishArtifactsPath
		Publish-Solution -path $SolutionOrProjectPaths -versionNumber $VersionNumber -publishArtifactsPath $PublishArtifactsPath

		Write-Status "Build script completed successfully."
	}
	catch {
		[string] $errorMessage = $_.Exception.Message
		Write-Error "The build script did not complete successfully. Error: $errorMessage. See output above for more details."
	}
}

begin {
	function Find-SolutionOrProjectFiles([string] $path) {
		$files = Get-ChildItem -Path $path -Recurse -Include *.sln

		if ($files.Count -eq 0) {
			Write-Verbose "No solution files found in the path '$path'. Searching for project files instead."
			$files = Get-ChildItem -Path $path -Recurse -Include *.csproj
		}

		if ($files.Count -eq 0) {
			throw "No solution or project files found in the path '$path'."
		}

		return $files
	}

	function Remove-PublishedArtifacts([string] $path) {
		if (Test-Path $path -PathType Container) {
			Write-Status "Cleaning published artifacts at '$path'."
			Remove-Item -Path $path -Recurse -Force
		}
	}

	function Clean-Solution([string[]] $paths) {
		foreach ($path in $paths) {
			Write-Status "Cleaning '$(Split-Path -Path $path -Leaf)'."
			Invoke-CommandAndThrowAnyErrors {
				& dotnet clean "$path"
			}
		}
	}

	function Restore-NuGetPackages([string[]] $paths) {
		foreach ($path in $paths) {
			Write-Status "Restoring NuGet packages for '$(Split-Path -Path $path -Leaf)'."
			Invoke-CommandAndThrowAnyErrors {
				& dotnet restore "$path" --interactive
			}
		}
	}

	function Build-Solution([string[]] $paths, [string] $versionNumber) {
		foreach ($path in $paths) {
			Write-Status "Building '$(Split-Path -Path $path -Leaf)'."
			Invoke-CommandAndThrowAnyErrors {
				& dotnet build "$path" -p:AssemblyVersion=$versionNumber -p:Version=$versionNumber --configuration Release
			}
		}
	}

	function Test-Solution([string[]] $paths, [string] $publishTestResultsPath) {
		foreach ($path in $paths) {
			Write-Status "Testing '$(Split-Path -Path $path -Leaf)'."
			Invoke-CommandAndThrowAnyErrors {
				& dotnet test "$path" --no-build --no-restore --configuration Release
			}
		}
	}

	function Publish-Solution([string[]] $paths, [string] $versionNumber, [string] $publishArtifactsPath) {
		foreach ($path in $paths) {
			Write-Status "Publishing '$(Split-Path -Path $path -Leaf)'."
			Invoke-CommandAndThrowAnyErrors {
				& dotnet publish "$path" --configuration Release -p:AssemblyVersion=$versionNumber -p:Version=$versionNumber --artifacts-path "$publishArtifactsPath"
			}
		}
	}

	function Wait-ForUserInput([string] $message) {
		# If running in the console, wait for input before closing.
		if ($Host.Name -eq "ConsoleHost") {
			Write-Host $message
			$Host.UI.RawUI.FlushInputBuffer() # Make sure buffered input doesn't "press a key" and skip the ReadKey().
			$Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyUp") > $null
		}
	}

	function Write-Status([string] $message) {
		$Host.UI.RawUI.WindowTitle = $message
		Write-Information $message
	}

	function Invoke-CommandAndThrowAnyErrors([scriptblock] $command) {
		Invoke-Command -ScriptBlock $command -ErrorVariable nonTerminatingErrors

		if ($nonTerminatingErrors) {
			throw "Errors occurred while running the command: $nonTerminatingErrors"
		}
		if ($LASTEXITCODE -ne 0) {
			throw "The command exited with code '$LASTEXITCODE'."
		}
	}

	$InformationPreference = 'Continue'
	# $VerbosePreference = 'Continue' # Uncomment this line if you want to see verbose messages.

	# Log all script output to a file for easy reference later if needed.
	[string] $lastRunLogFilePath = "$PSCommandPath.LastRun.log"
	Start-Transcript -Path $lastRunLogFilePath

	# Display the time that this script started running.
	[DateTime] $startTime = Get-Date
	Write-Information "Starting script at '$($startTime.ToString('u'))'."
}

end {
	# Display the time that this script finished running, and how long it took to run.
	[DateTime] $finishTime = Get-Date
	[TimeSpan] $elapsedTime = $finishTime - $startTime
	Write-Information "Finished script at '$($finishTime.ToString('u'))'. Took '$elapsedTime' to run."

	Stop-Transcript

	if (-not $NoPromptBeforeExiting) {
		Wait-ForUserInput -message "Press any key to continue..."
	}
}
