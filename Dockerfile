FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /KakeysBakery
 
EXPOSE 8080
 
COPY KakeysBakery/KakeysBakery.csproj .
RUN dotnet restore
 
COPY . .
WORKDIR /KakeysBakery/KakeysBakery
RUN dotnet build -c Release -o /app/build
 
FROM build as publish
RUN dotnet publish -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=publish /app/publish .
 
ENTRYPOINT ["dotnet", "KakeysBakery.dll"]