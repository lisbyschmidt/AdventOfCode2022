FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /app
COPY day07.csproj .
COPY input.txt .
COPY Program.cs .
RUN apk add clang build-base zlib-dev
RUN dotnet publish -c Release -o out -p:'PublishAot=true;StripSymbols=true;InvariantGlobalization=true' -r linux-musl-x64

FROM alpine:latest
WORKDIR /app
COPY --from=build-env /app/out .
RUN apk add libstdc++
ENTRYPOINT ./day07
