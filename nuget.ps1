del TsSoft.Commons.*.nupkg
del *.nuspec
del .\TsSoft.Commons\bin\Release\*.nuspec

function GetNodeValue([xml]$xml, [string]$xpath)
{
	return $xml.SelectSingleNode($xpath).'#text'
}

function SetNodeValue([xml]$xml, [string]$xpath, [string]$value)
{
	$node = $xml.SelectSingleNode($xpath)
	if ($node) {
		$node.'#text' = $value
	}
}

Remove-Item .\TsSoft.Commons\bin -Recurse 
Remove-Item .\TsSoft.Commons\obj -Recurse

$build = "c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe ""TsSoft.Commons\TsSoft.Commons.csproj"" /p:Configuration=Release" 
Invoke-Expression $build

$Artifact = (resolve-path ".\TsSoft.Commons\bin\Release\TsSoft.Commons.dll").path

nuget spec -F -A $Artifact

Copy-Item .\TsSoft.Commons.nuspec.xml .\TsSoft.Commons\bin\Release\TsSoft.Commons.nuspec

$GeneratedSpecification = (resolve-path ".\TsSoft.Commons.nuspec").path
$TargetSpecification = (resolve-path ".\TsSoft.Commons\bin\Release\TsSoft.Commons.nuspec").path

[xml]$srcxml = Get-Content $GeneratedSpecification
[xml]$destxml = Get-Content $TargetSpecification
$value = GetNodeValue $srcxml "//version"
SetNodeValue $destxml "//version" $value;
$value = GetNodeValue $srcxml "//description"
SetNodeValue $destxml "//description" $value;
$value = GetNodeValue $srcxml "//copyright"
SetNodeValue $destxml "//copyright" $value;
$destxml.Save($TargetSpecification)

nuget pack $TargetSpecification

del *.nuspec
del .\TsSoft.Commons\bin\Release\*.nuspec

exit
