# FROM mcr.microsoft.com/dotnet/aspnet:7.0
# COPY ./dist /app
# COPY wait-for-it.sh /app/wait-for-it.sh
# RUN chmod +x /app/wait-for-it.sh
# WORKDIR /app
# EXPOSE 80
# ENV WAITHOST=mysql WAITPORT=3306
# ENTRYPOINT ./wait-for-it.sh $WAITHOST:$WAITPORT --timeout=0 \
#     && exec dotnet ExampleApp.dll
#ENTRYPOINT [ "dotnet","ExampleApp.dll" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY ["ExampleApp.csproj","./"]
RUN dotnet restore "ExampleApp.csproj"
COPY . .
WORKDIR /src/.
RUN dotnet build "ExampleApp.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "ExampleApp.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ./wait-for-it.sh $WAITHOST:$WAITPORT --timeout=0 \&& exec dotnet ExampleApp.dll