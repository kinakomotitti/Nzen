FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ENV ConnectionString="Host=localhost;Port=5432;Username=postgres;Password=p@ssw0rd; Database=nzen-db" \
    SpeechAPI_Key="859b3e1bc5514b60819d09a4db722436" \
	SpeechAPI_Region="eastus"
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nzen/Nzen.csproj", "Nzen/"]
RUN dotnet restore "Nzen/Nzen.csproj"
COPY . .
WORKDIR "/src/Nzen"
RUN dotnet build "Nzen.csproj" -c Release -o /app


FROM build AS publish
RUN dotnet publish "Nzen.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Nzen.dll"]
