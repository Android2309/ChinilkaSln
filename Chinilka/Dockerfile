FROM mcr.microsoft.com/dotnet/aspnet:6.0

COPY /bin/Release/net6.0/publish/ Chinilka/

ENV ASPNETCORE_ENVIRONMENT Production
ENV Logging__Console__FormatterName=Simple

EXPOSE 5000
WORKDIR /Chinilka
ENTRYPOINT ["dotnet", "Chinilka.dll", "--urls=http://0.0.0.0:5000"]