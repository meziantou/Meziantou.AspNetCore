dotnet restore
dotnet pack src\Meziantou.AspNetCore.Hsts\Meziantou.AspNetCore.Hsts.csproj --output ../../nupkgs --configuration Release
dotnet pack src\Meziantou.AspNetCore.LetsEncrypt\Meziantou.AspNetCore.LetsEncrypt.csproj --output ../../nupkgs --configuration Release
dotnet pack src\Meziantou.AspNetCore.Mvc.TagHelpers\Meziantou.AspNetCore.Mvc.TagHelpers.csproj --output ../../nupkgs --configuration Release
dotnet pack src\Meziantou.AspNetCore.Rewrite.Rules\Meziantou.AspNetCore.Rewrite.Rules.csproj --output ../../nupkgs --configuration Release
dotnet pack src\Meziantou.AspNetCore.UseNodeModules\Meziantou.AspNetCore.UseNodeModules.csproj --output ../../nupkgs --configuration Release