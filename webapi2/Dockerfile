# docker run -d -p 8080:80 --name myapp chandrabhan/webapi2
# docker run -d -p 8080:80  chandrabhan/webapi2 "ExtraSettingNotInSettingsFile=192.0.1.2"
FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "webapi2.dll"]
