$part = $ENV:part
$inputContent = Get-Content ./input.txt -Raw

$caloryArrayPrElf = $inputContent.Split([System.Environment]::NewLine + [System.Environment]::NewLine)
$calorySumPrElf = $caloryArrayPrElf | foreach { $_.Split([System.Environment]::NewLine) | measure -Sum }

Write-Host "PowerShell"
if ($part -eq "part1") {
    $result = $calorySumPrElf | measure { $_.Sum } -Maximum
    Write-Host $result.Maximum
}
if ($part -eq "part2") {
    $result = $calorySumPrElf | Sort-Object { [int]$_.Sum } -Descending | select -First 3 | measure { $_.Sum } -Sum
    Write-Host $result.Sum
}