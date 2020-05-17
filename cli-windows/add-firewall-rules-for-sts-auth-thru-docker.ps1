param([switch]$Elevated)
function Check-Admin {
$currentUser = New-Object Security.Principal.WindowsPrincipal $([Security.Principal.WindowsIdentity]::GetCurrent())
$currentUser.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
}
if ((Check-Admin) -eq $false)  {
if ($elevated)
{
# could not elevate, quit
}
 
else {
 
Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
}
exit
}

try {
  Get-NetFirewallRule -DisplayName EshopDocker -ErrorAction Stop
  Write-Host "Rule found"
}
  catch [Exception] {
  New-NetFirewallRule -DisplayName eShop-Inbound -Confirm -Description "eShop Inbound Rule for port range 5100-5150" -LocalAddress Any -LocalPort 5100-5150 -Protocol tcp -RemoteAddress Any -RemotePort Any -Direction Inbound
  New-NetFirewallRule -DisplayName eShop-Outbound -Confirm -Description "eShop Outbound Rule for port range 5100-5150" -LocalAddress Any -LocalPort 5100-5150 -Protocol tcp -RemoteAddress Any -RemotePort Any -Direction Outbound
}