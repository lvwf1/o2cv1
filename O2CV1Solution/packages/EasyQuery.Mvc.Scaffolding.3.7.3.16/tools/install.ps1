param($rootPath, $toolsPath, $package, $project)

# Bail out if scaffolding is disabled (probably because you're running an incompatible version of NuGet)
if (-not (Get-Command Invoke-Scaffolder)) { return }

# Could use "Set-DefaultScaffolder" here if desired


$EfVersion = "#"

#find out the version of EntityFramework assembly in the target project
$project.Object.References | Where-Object { $_.Name -eq 'EntityFramework' } | ForEach-Object { $EfVersion = $_.Version}
$EfAssemblyName = "Korzh.EasyQuery.EF" + $EfVersion[0]
if ($EfVersion -ne '#') {
	Write-Host "EntityFramework version $EfVersion detected"
	Write-Host "Removing all EF assemblies except $EfAssemblyName"
}
else {
	Write-Host "EntityFramework is not used in this project"
	Write-Host "Removing all Korzh.EasyQuery.EF# assemblies"
}

#removing unnecessary Korzh.EasyQuery.EF# assemblies from project references
$project.Object.References | Where-Object { ($_.Name.StartsWith("Korzh.EasyQuery.EF")) -and !($_.Name.StartsWith($EfAssemblyName)) } | ForEach-Object { 
	$_.Remove()
}

