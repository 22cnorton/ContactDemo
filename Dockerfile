FROM mcr.microsoft.com/dotnet/sdk:10.0 
EXPOSE 8080
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile", "--no-hot-reload"]
