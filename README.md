
```dotnetcli

$ dotnet workload update

$ dotnet workload install aspire

$ dotnet workload list

$ dotnet new list aspire

# install podman https://github.com/containers/podman/blob/main/docs/tutorials/podman-for-windows.md
$ PS C:\Users\User> podman machine init

$ podman images

$ podman ps

$ dotnet new aspire-starter --use-redis-cache --output AspireSample    

# 
$ dotnet run --project AspireSample/AspireSample.AppHost

$ dotnet add AspireSample.AppHost package Aspire.Hosting.PostgreSQL --prerelease    

$ dotnet add AspireSample.AppHost package Aspire.Hosting.MongoDB --prerelease    

$ dotnet add AspireSample.AppHost package Aspire.Hosting.Kafka --prerelease

$ dotnet add  AspireSample.AppHost package Aspire.Hosting.MySql  --prerelease

$ dotnet new webapi -o TodoApi

$ dotnet sln add .\TodoApi\

$ dotnet add .\TodoApi\ package Marten
```