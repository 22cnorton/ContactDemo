FROM mcr.microsoft.com/dotnet/sdk:10.0 
EXPOSE 8080
WORKDIR /src

ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile", "--no-hot-reload"]
