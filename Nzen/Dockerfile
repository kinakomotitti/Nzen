FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ENV ConnectionString="Host=172.17.0.2;Port=5432;Username=postgres;Password=postgres; Database=nzen_db" \
    SpeechAPI_Key="b922769d8f6f45a7a7f8143c4126384c" \
	SpeechAPI_Region="japaneast" \
	UseDataBase="true"
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
