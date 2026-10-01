$ErrorActionPreference = "Stop"

$ResourceGroup = "LinuxBuilder-Free-RG"
$Location = "eastus"

$ApiAppName = "linuxbuilder-api-$((Get-Random -Maximum 9999))"

az login

az group create `
    --name $ResourceGroup `
    --location $Location

# Free App Service Plan

az appservice plan create `
    --resource-group $ResourceGroup `
    --name linuxbuilder-free-plan `
    --sku F1

# Free Web App

az webapp create `
    --resource-group $ResourceGroup `
    --plan linuxbuilder-free-plan `
    --name $ApiAppName `
    --runtime "DOTNETCORE:8.0"

# Publish API

dotnet publish .\Api `
    -c Release `
    -o .\publish

Compress-Archive `
    -Path .\publish\* `
    -DestinationPath .\api.zip `
    -Force

az webapp deploy `
    --resource-group $ResourceGroup `
    --name $ApiAppName `
    --src-path .\api.zip `
    --type zip

Write-Host ""
Write-Host "API URL:"
Write-Host "https://$ApiAppName.azurewebsites.net"