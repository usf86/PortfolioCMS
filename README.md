# PortfolioCMS — Site portfolio configurable multi-métiers

Solution complète : **.NET 8 Web API** + **Angular 18** + **MySQL (EF Core Code-First)**.
Un seul moteur de code, réutilisable pour n'importe quel petit commerce
(boulanger, boucher, garagiste, coiffeur...) grâce à un modèle de données générique.

```
PortfolioCMS/
├── backend/                  → Solution .NET (Domain / Application / Infrastructure / Api)
├── frontend/portfolio-app/   → Application Angular standalone
├── .vscode/                  → Tâches, debug, extensions recommandées
└── PortfolioCMS.code-workspace
```

## 🚀 Démarrage rapide dans VS Code

1. **Ouvrez le fichier `PortfolioCMS.code-workspace`** dans VS Code (recommandé,
   sépare proprement les dossiers backend/frontend dans l'explorateur).
2. Installez les extensions recommandées si la popup apparaît (C# Dev Kit,
   Angular Language Service, Tailwind CSS IntelliSense).

### Backend (.NET API)

Prérequis : [.NET 8 SDK](https://dotnet.microsoft.com/download), MySQL Server, `dotnet-ef` CLI :
```bash
dotnet tool install --global dotnet-ef
```

Depuis `Terminal > Run Task...` dans VS Code, lancez dans l'ordre :
1. `restore-backend` — restaure les packages NuGet
2. Modifiez la chaîne de connexion dans `backend/src/PortfolioCMS.Api/appsettings.json`
3. `ef-migrations-add-InitialCreate` — génère la première migration
4. `ef-database-update` — crée la base `portfolio_cms` sur votre serveur MySQL

Puis lancez l'API avec **F5** (configuration "API (.NET) - Lancer" déjà fournie dans `.vscode/launch.json`),
ou en ligne de commande :
```bash
dotnet run --project backend/src/PortfolioCMS.Api
```
→ Swagger : http://localhost:5080/swagger
→ Au premier démarrage en mode Development, deux profils de démo sont insérés
  automatiquement : **Boulangerie Dupont** et **Garage Lefort**.

### Frontend (Angular)

Prérequis : [Node.js 18+](https://nodejs.org/) et npm.

Depuis `Terminal > Run Task...` :
1. `install-frontend` — installe les dépendances npm
2. `serve-frontend` — démarre `ng serve` sur http://localhost:4200

Ou en ligne de commande :
```bash
cd frontend/portfolio-app
npm install
npm start
```

Le composant `HomeComponent` appelle `GET /api/portfolio/by-activity/boulangerie`
au démarrage — modifiez la valeur `activityType` dans `home.component.ts` pour
basculer sur `garage` et voir le même front s'adapter à un autre métier.

## 📡 Endpoints API

| Méthode | Route | Description |
|---|---|---|
| GET | `/api/portfolio/{id}` | Configuration complète par identifiant de profil |
| GET | `/api/portfolio/by-activity/{activityType}` | Configuration complète par type d'activité (ex: `boulangerie`, `garage`) |

## 🧱 Modèle de données

- **BusinessProfile** : identité, couleurs, contact, horaires
- **ContentSection** : sections dynamiques de la page (À propos, Galerie, etc.)
- **ProductOrService** : items génériques (produit, prestation, pièce détachée...)
- **Testimonial** : avis clients, avec modération (`IsApproved`)

## 🔭 Pistes d'évolution

- Back-office d'administration Angular (CRUD + auth JWT)
- Upload d'images (logo, galerie) vers stockage local ou cloud (S3/Azure Blob)
- Cache (`IMemoryCache` / Redis) sur `GetFullConfigAsync`
- Multi-tenant strict par sous-domaine
