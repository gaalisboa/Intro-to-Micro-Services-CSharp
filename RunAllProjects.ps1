# Script for running multiple projects in VSCode (powershell)

start powershell { dotnet watch run --project .\FrontEnd\GeekShopping.Web\GeekShopping.Web.csproj }
start powershell { dotnet watch run --project .\Services\GeekShopping.IdentityServer\GeekShopping.IdentityServer.csproj }
start powershell { dotnet watch run --project .\Services\GeekShopping.ProductAPI\GeekShopping.ProductAPI.csproj }