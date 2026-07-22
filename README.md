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
