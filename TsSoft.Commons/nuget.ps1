$Artifact = (resolve-path ".\bin\Release\TsSoft.Commons.dll").path
$Specification = (resolve-path ".\TsSoft.Commons.nuspec").path

del TsSoft.Commons.*.nupkg
del TsSoft.Commons.nuspec

nuget spec -F -A $Artifact

function RemoveNode([xml]$xml, [string]$xpath)
{
	$node = $xml.SelectSingleNode($xpath)
	if ($node) {
		[Void]$node.ParentNode.RemoveChild($node)
	}
}

function SetNodeValue([xml]$xml, [string]$xpath, [string]$value)
{
	$node = $xml.SelectSingleNode($xpath)
	if ($node) {
		$node.'#text' = $value
	}
}

[xml]$xml = Get-Content $Specification
RemoveNode $xml "//licenseUrl"
RemoveNode $xml "//iconUrl"
RemoveNode $xml "//releaseNotes"
RemoveNode $xml "//dependency[@id='SampleDependency']"
SetNodeValue $xml "//projectUrl" "http://ts-soft.ru/dev/commons/"
SetNodeValue $xml "//tags" "Common Utility"
$xml.Save($Specification)

nuget pack TsSoft.Commons.csproj -Prop Configuration=Release
del TsSoft.Commons.nuspec

exit