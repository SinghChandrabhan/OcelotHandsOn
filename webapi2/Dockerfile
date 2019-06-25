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

# docker run -it -v c:/Data:/app/dataVol  chandrabhan/aspnetapp  // notice full path
# docker run -it -v myvol:/app/dataVol  chandrabhan/aspnetapp  //for windows container


# docker run -it --mount type=bind,source=c:/Data,target=/app/dataVol chandrabhan/aspnetapp
# docker create vol
# docker run -it --mount source=myvol,target=/app/dataVol  chandrabhan/aspnetapp

# create volume
#az container create \
#--resource-group myResourceGroup \
#--name cloud-workloadapi_2 \
#--image acrapiworkload.azurecr.io/cloudworkload-dotnetapi \
#--cpu 1 \
#--memory 1 \
#--registry-login-server acrapiworkload.azurecr.io \
#--registry-username acrapiworkload \
#--registry-password 6giHm2XByEOjc3gW+6j=qE5erMiCbEdG \
#--dns-name-label dockerworkloadapi \
#--ports 80
#--azure-file-volume-account-name filesharestorageacc \
#--azure-file-volume-account-key WIcSxPJcGfOQvSvCjpiT0tejkBHiw2C2ad+BWNp09WEuSJ4r0ZB5D+lUiufMXjZBclJOQPqWBpe02qu0eq8/bA== \
#--azure-file-volume-share-name dockervol \
#--azure-file-volume-mount-path /app/vol
#
