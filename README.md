# TarotProject

En webbaserad tarotapplikation byggd med ASP.NET Core MVC. Utforska hela Rider-Waite-leken, dra dagens kort eller gör en trekortsläggning för dåtid, nutid och framtid.

Kort kan visas upprätt eller omvänt och presenteras med en tillhörande svensk tolkning.

## Funktioner

- Utforska en komplett tarotlek med 78 kort
- Visa kortens namn, svit, nummer, bild och betydelse
- Dra ett slumpmässigt kort för dagen
- Gör en trekortsläggning för dåtid, nutid och framtid
- Slumpa mellan upprätt och omvänd position
- Läs olika tolkningar beroende på kortets position
- Lagra kort och betydelser lokalt i en SQLite-databas
- Fylla databasen automatiskt från JSON när applikationen startar

## Teknik

- C# och .NET 10
- ASP.NET Core MVC
- Entity Framework Core 9
- SQLite
- Razor Views
- Bootstrap
- HTML och CSS

## Kom igång

### Förutsättningar

Installera följande innan du börjar:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Git](https://git-scm.com/downloads)

### Installation

1. Klona projektet:

   ```bash
   git clone https://github.com/ArtByMagicUnicorn/TarotProject.git
   cd TarotProject
   ```

2. Återställ projektets paket:

   ```bash
   dotnet restore
   ```

3. Starta webbapplikationen:

   ```bash
   dotnet run --project Tarot.Web
   ```

4. Öppna adressen som visas i terminalen. Med projektets nuvarande utvecklingsinställningar används vanligtvis:

   - `https://localhost:7222`
   - `http://localhost:5188`

SQLite-databasen använder anslutningen `DataSource=tarot.db`. Kortleken och kortens tolkningar läses in automatiskt när applikationen startar.

## Användning

I navigeringsmenyn finns tre huvudsakliga val:

- **Ett Kort** drar ett slumpmässigt kort med en upprätt eller omvänd tolkning.
- **Tre Kort** skapar en läggning för dåtid, nutid och framtid.
- **Hela Kortleken** visar samtliga 78 kort och länkar till mer information om varje kort.

## Projektstruktur

```text
TarotProject/
├── Tarot.Data/        # Datamodeller, DbContext, migreringar och startdata
├── Tarot.Web/         # MVC-applikation, controllers, views och statiska filer
├── TarotProjekt.slnx  # Solution-fil
└── README.md
```

## Under utveckling

Projektet utvecklas fortfarande. Bland funktionerna som förbereds finns:

- Keltiskt kors med tio kort
- Möjlighet att spara egna anteckningar i en tarotdagbok
- Fortsatt förbättring av design och användarupplevelse

## Ansvarsfriskrivning

Tarottolkningarna i projektet är avsedda för inspiration, reflektion och underhållning.

## Skapare

Skapat av [ArtByMagicUnicorn](https://github.com/ArtByMagicUnicorn).
