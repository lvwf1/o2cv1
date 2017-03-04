[T4Scaffolding.Scaffolder(Description = "This Scaffolder creates an EasyQuery View")][CmdletBinding()]
param(
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ViewName,
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ControllerName,
    [string]$Project,
    [string]$CodeLanguage,
    [string[]]$TemplateFolders,
    [switch]$Force = $false
)
 
$outputPath = "Views\$ControllerName\$ViewName" 
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
Add-ProjectItemViaTemplate -OutputPath $outputPath -Template EasyQueryViewTemplate `
    -Model @{ PluginName = "EasyQuery"; ControllerName = $ControllerName; Namespace = $namespace; } `
    -SuccessMessage "Added EasyQuery view {0}" `
    -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

 
