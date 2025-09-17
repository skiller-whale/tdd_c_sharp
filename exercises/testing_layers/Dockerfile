FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y nodejs

RUN npm install -g playwright \
    && playwright install-deps \
    && playwright install chromium
