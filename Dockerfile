### STAGE 1: Build DotNet Core ###
FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env

ARG APP_VER

COPY src /app/src
COPY Blazing.DotNet.Tweets.sln /app/Blazing.DotNet.Tweets.sln
#COPY NuGet.config /app/NuGet.config
WORKDIR /app

RUN dotnet restore

WORKDIR /app/src/Blazing.DotNet.Tweets

RUN echo ${APP_VER}
RUN dotnet publish -o /publish -c Release -f net5.0 -r debian.10-x64 /p:Version=$APP_VER /p:InformationalVersion=$APP_VER

### STAGE 2: Runtime ###
FROM mcr.microsoft.com/dotnet/runtime:latest

# Set environment variables

WORKDIR /app
COPY --from=build-env /publish .

EXPOSE 5000 5001

ENTRYPOINT ["dotnet", "Blazing.DotNet.Tweets.AppServer.dll"]