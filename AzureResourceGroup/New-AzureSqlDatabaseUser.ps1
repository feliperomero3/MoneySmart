#Requires -Version 3.0

Param(
    [string] [Parameter(Mandatory=$true)] $SQLServerName,
    [string] [Parameter(Mandatory=$true)] $DatabaseName,
    [string] [Parameter(Mandatory=$true)] $AdminLogin,
    [string] [Parameter(Mandatory=$true)] $AdminPassword,
    [string] [Parameter(Mandatory=$true)] $UserLogin,
    [string] [Parameter(Mandatory=$true)] $UserPassword
)

Write-Host "Create SQL connection string"
$conn = New-Object System.Data.SqlClient.SQLConnection 
$conn.ConnectionString = "Data Source=$SQLServerName.database.windows.net,1433;Initial Catalog=$DatabaseName;User ID=$AdminLogin;Password=$AdminPassword;Connect Timeout=30"
Write-host "Connect to database and execute SQL script"

try
{
    $conn.Open()
    $ddlstmt = `
    "IF NOT EXISTS ( 
        SELECT name
        FROM sys.database_principals
        WHERE name = '$UserLogin'
    )
    BEGIN
        CREATE USER $UserLogin WITH PASSWORD = '$UserPassword';
    END;
    ALTER ROLE db_datareader ADD member $UserLogin;
    ALTER ROLE db_datawriter ADD member $UserLogin;"
    $command = New-Object -TypeName System.Data.SqlClient.SqlCommand($ddlstmt, $conn)
    $command.ExecuteNonQuery() | Out-Null
    Write-host "SQL script executed successfully"
    $conn.Close()
}
catch [Exception]
{
    Write-Error $_.Exception.Message
    Write-Error $command
}
finally
{
    $conn.Dispose()
    $command.Dispose()
}
