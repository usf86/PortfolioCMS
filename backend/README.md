# PortfolioCMS — Backend (.NET 8 / EF Core / MySQL)

## Prerequis
- .NET 8 SDK
- MySQL Server (8.x) en local ou via Docker
- Outil EF Core CLI : `dotnet tool install --global dotnet-ef`

## Configuration
1. Modifiez la chaine de connexion dans `src/PortfolioCMS.Api/appsettings.json`.
2. Generez la base de donnees :
   ```bash
   cd src/PortfolioCMS.Api
   dotnet ef migrations add InitialCreate --project ../PortfolioCMS.Infrastructure --startup-project .
   dotnet ef database update --project ../PortfolioCMS.Infrastructure --startup-project .
   ```
3. Lancez l'API :
   ```bash
   dotnet run --project src/PortfolioCMS.Api
   ```
   Swagger disponible sur : http://localhost:5080/swagger

Au premier lancement en environnement "Development", les donnees de demonstration
(Boulangerie Dupont + Garage Lefort) sont automatiquement inserees.

## Endpoints principaux
- `GET /api/portfolio/{id}` — configuration complete par identifiant
- `GET /api/portfolio/by-activity/{activityType}` — ex: `boulangerie`, `garage`
