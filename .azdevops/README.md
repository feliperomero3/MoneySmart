# Provision a web app with a SQL Database

This template creates a free Azure Web App with Application Insights and Azure SQL Database at the "Free" service level.
Supports other tiers of service through the use of parameters.

List available SKUs (DTU and vCores) for Azure SQL Database for a given Azure region.

PowerShell

```pwsh
Get-AzSqlServerServiceObjective -Location 'South Central US'
```

More info at <https://docs.microsoft.com/en-us/powershell/module/az.sql/get-azsqlserverserviceobjective>

Azure CLI

```bash
az sql db list-editions -location "South Central US" -output table
```

More info at <https://docs.microsoft.com/en-us/azure/templates/microsoft.sql/2019-06-01-preview/servers/databases?pivots=deployment-language-arm-template#sku-1>
