FROM mcr.microsoft.com/dotnet/runtime:6.0.12-jammy-arm64v8
WORKDIR /app
COPY bin/Release/net6.0/publish/ ./

ENTRYPOINT ["dotnet", "DockerTest.dll"]
