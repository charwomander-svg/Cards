# Cards

The Ultimate Card Game Collection is a large card-game catalog and rules-engine prototype.

## Itch.io demo build

The fastest shippable target is the terminal demo in `src\Cards.Itch`. It lets players browse the catalog, read rules, start supported playable engines, use hints/autoplay, and save/load within the session.

Run locally:

```powershell
dotnet run --project .\src\Cards.Itch\Cards.Itch.csproj
```

Publish a Windows folder build for itch.io:

```powershell
dotnet publish .\src\Cards.Itch\Cards.Itch.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o .\artifacts\itch-win-x64
```

Zip the contents of `artifacts\itch-win-x64` and upload that zip to itch.io as the Windows demo build.

Useful in-demo commands:

`list`, `search <text>`, `category next`, `select <number>`, `rules`, `players <n>`, `start`, `actions`, `play <number>`, `hints`, `auto [n]`, `save`, `load`, `status`, `quit`.

## Full web deployment (backend + UI)

The browser UI requires the ASP.NET backend (`/api/*`), so deploy the full `web-adapter` service (not static HTML only).

### Render deployment

This repo includes:

- `Dockerfile` - builds/publishes `web-adapter` and runs `WebAdapter.dll`
- `render.yaml` - Render Blueprint service definition

Deploy steps:

1. Push the repo branch to GitHub.
2. In Render, create a new Blueprint and select this repository.
3. Render reads `render.yaml`, builds the Docker image, and deploys the service.
4. Open the deployed URL root (`/`) for the web UI.
5. Use that URL as the external web link from your itch page.
