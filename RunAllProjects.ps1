# Script for running multiple projects in VSCode (powershell)

Start-Process powershell { dotnet watch run --project .\FrontEnd\GeekShopping.Web\GeekShopping.Web.csproj }
Start-Process powershell { dotnet watch run --project .\Services\GeekShopping.IdentityServer\GeekShopping.IdentityServer.csproj }
Start-Process powershell { dotnet watch run --project .\Services\GeekShopping.ProductAPI\GeekShopping.ProductAPI.csproj }