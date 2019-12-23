if ($PSEdition -eq 'Desktop') {
    try {
	    [Microsoft.Azure.PowerShell.Authenticators.DesktopAuthenticatorBuilder]::Apply([Microsoft.Azure.Commands.Common.Authentication.AzureSession]::Instance)
	} catch {}
}

Write-Host "Hello after load"

if ($PSEdition -eq 'Core') {
    try {
	    [Microsoft.Azure.Commands.Profile.Utilities.CustomAssemblyResolver]::Initialize()
	} catch {
    Write-Host "Hello after load error"
}
}

