#
# Module manifest for module 'PSGet_AzureRM.RecoveryServices.Backup'
#
# Generated by: Microsoft Corporation
#
# Generated on: 3/20/2018
#

@{

# Script module or binary module file associated with this manifest.
# RootModule = ''

# Version number of this module.
ModuleVersion = '4.1.1'

# Supported PSEditions
# CompatiblePSEditions = @()

# ID used to uniquely identify this module
GUID = '3d444bee-6742-4ce2-b573-dfd1b7c4144c'

# Author of this module
Author = 'Microsoft Corporation'

# Company or vendor of this module
CompanyName = 'Microsoft Corporation'

# Copyright statement for this module
Copyright = 'Microsoft Corporation. All rights reserved.'

# Description of the functionality provided by this module
Description = 'Microsoft Azure PowerShell - Azure Backup service cmdlets for Azure Resource Manager'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
DotNetFrameworkVersion = '4.5.2'

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
CLRVersion = '4.0'

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
RequiredModules = @(@{ModuleName = 'AzureRM.Profile'; ModuleVersion = '4.5.0'; })

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = '.\Microsoft.Azure.Commands.RecoveryServices.ARM.dll', 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.Models.dll', 
               '.\Microsoft.Azure.Management.RecoveryServices.Backup.dll', 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.Helpers.dll', 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.Logger.dll', 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.Providers.dll', 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.ServiceClientAdapter.dll', 
               '.\Microsoft.Azure.Management.RecoveryServices.dll', 
               '.\Security.Cryptography.dll'

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = 
               '.\Microsoft.Azure.Commands.RecoveryServices.Backup.format.ps1xml'

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('.\Microsoft.Azure.Commands.RecoveryServices.Backup.dll')

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @()

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = 'Backup-AzureRmRecoveryServicesBackupItem', 
               'Get-AzureRmRecoveryServicesBackupManagementServer', 
               'Get-AzureRmRecoveryServicesBackupContainer', 
               'Unregister-AzureRmRecoveryServicesBackupContainer', 
               'Disable-AzureRmRecoveryServicesBackupProtection', 
               'Enable-AzureRmRecoveryServicesBackupProtection', 
               'Get-AzureRmRecoveryServicesBackupItem', 
               'Get-AzureRmRecoveryServicesBackupJob', 
               'Get-AzureRmRecoveryServicesBackupJobDetails', 
               'Stop-AzureRmRecoveryServicesBackupJob', 
               'Wait-AzureRmRecoveryServicesBackupJob', 
               'Get-AzureRmRecoveryServicesBackupProtectionPolicy', 
               'Get-AzureRmRecoveryServicesBackupRetentionPolicyObject', 
               'Get-AzureRmRecoveryServicesBackupSchedulePolicyObject', 
               'New-AzureRmRecoveryServicesBackupProtectionPolicy', 
               'Remove-AzureRmRecoveryServicesBackupProtectionPolicy', 
               'Set-AzureRmRecoveryServicesBackupProtectionPolicy', 
               'Get-AzureRmRecoveryServicesBackupRecoveryPoint', 
               'Restore-AzureRmRecoveryServicesBackupItem', 
               'Unregister-AzureRmRecoveryServicesBackupManagementServer', 
               'Get-AzureRmRecoveryServicesBackupRPMountScript', 
               'Disable-AzureRmRecoveryServicesBackupRPMountScript'

# Variables to export from this module
# VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @()

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'Azure','ResourceManager','ARM','RecoveryServices','Backup'

        # A URL to the license for this module.
        LicenseUri = 'https://aka.ms/azps-license'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/Azure/azure-powershell'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = '* Fixed issue with cleaning up scripts in build'

        # Prerelease string of this module
        # Prerelease = ''

        # Flag to indicate whether the module requires explicit user acceptance for install/update
        # RequireLicenseAcceptance = $false

        # External dependent modules of this module
        # ExternalModuleDependencies = @()

    } # End of PSData hashtable
    
 } # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

