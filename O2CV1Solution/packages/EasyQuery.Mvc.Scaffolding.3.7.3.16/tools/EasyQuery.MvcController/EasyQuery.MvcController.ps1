[T4Scaffolding.Scaffolder(Description = "This Scaffolder create a EasyQuery Controller and View")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ControllerName,
	[string]$ConnectionName,
	[string]$DbContext,
	[switch]$NoView=$false,
    [string]$Project,
    [string]$CodeLanguage,
    [string[]]$TemplateFolders,
    [switch]$Force = $false
)

$ControllerFileName = $ControllerName + "Controller"

#if we work with DB directly 
if($ConnectionName.Length -ne 0){
   
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	
	Add-ProjectItemViaTemplate "Controllers\$ControllerFileName" -Template EasyQueryControllerDBTemplate `
	-Model @{ Namespace = $namespace; ConnectionName = $ConnectionName; ControllerName = $ControllerName  } `
	-SuccessMessage "Added $ControllerName " `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
}

#if EntityFramework is used
if ($DbContext.Length -ne 0){
 	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	
	Add-ProjectItemViaTemplate "Controllers\$ControllerFileName" -Template EasyQueryControllerEFTemplate `
	-Model @{ Namespace = $namespace; ContextName = $DbContext; ControllerName =$ControllerName  } `
	-SuccessMessage "Added $ControllerName " `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
}
	
#Creating View    
if(!$NoView.IsPresent){
	Scaffold EasyQuery.View -ViewName $ControllerName -ControllerName $ControllerName
}