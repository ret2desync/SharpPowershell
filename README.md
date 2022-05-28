# SharpPowershell
A C Sharp project and XMLBuild file to run a PowerShell CLI "clone" in Full Language Mode (bypassing set Constrained Language Mode on powershell.exe), bypassed/disabled AMSI without PowerShell Script Block Logging.

Full Language Mode is achieved via Custom Runspaces.

AMSI is bypassed by running a varation of Matt Graeber's amsiInitFailed method.

PowerShell Script Block Logging is bypassed by setting the ETW Provider to write to a newly created one.

## How to compile
If using the C Sharp project SharpPowershell, git clone this repo and build with Visual Studio.

If using the XMLBuild file, no compilation needed.

## How to run
Simply run the executeable to obtain a PowerShellish command line interace.

If using the XMLBuild varatiant run either:

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe msbuild-powershell.xml

Or:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe msbuild-powershell.xml
