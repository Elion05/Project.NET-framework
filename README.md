# MangaBookTracker Project ##

## Inleiding ##
Voor dit project heb ik een applicatie gemaakt waar lezers boeken kunnen opzoeken, meer informatie kunnen vinden over auteurs en het type genre.


## Functionaliteiten ##

### Backend & Data ##
*   **Framework**: .NET 9.0 (ASP.NET Core MVC).
*   **Database**: Entity Framework Core met SQL Server.
*   **Architectuur**: Ik heb een gescheiden Class Library (`MangaBook_Models`) gebruikt voor de domein-modellen en database-context.
*   **Asynchrone Verwerking**: Ik heb systematisch gebruik gemaakt van `async/await` in de controllers.
*   **Middleware**: Ik heb mijn eigen custom middleware (`MijnGebruiker`) geschreven voor request handling/gebruikersbeheer.
*   **Seeding**: Bij het opstarten wordt de database automatisch gevuld met dummy data (Users, Roles, Books, Authors, Genres).

### Frontend ##
*   **Framework**: Razor Views met Bootstrap styling.
*   **Interactie**:
    *   Zoeken en filteren op genre en auteur.
    *   Sorteren op titel en datum.
    *   Ik heb Asynchrone (Ajax) updates geïmplementeerd in de `EditBook` weergave.
*   **Meertaligheid**: Ik heb bijna 100% van de website vertaald naar het Nederlands, Engels en Frans.

### Security (Identity Framework) ##
*   **Gebruikersbeheer**: Ik heb een eigen `MangaUser` klasse gemaakt met extra eigenschappen (Naam, Taal).
*   **Rollen**: Ik heb 3 actieve rollen geïmplementeerd:
    *   `Admin`: Volledige rechten.
    *   `User`: Beperkte rechten.
    *   `System_Admin`: Beheerrechten.
*   **Controles**: Ik heb autorisatie toegepast op controllers en methoden, en functionaliteit om gebruikers te blokkeren/deblokkeren, zodat de users bijvoorbeeld niet in de Users/Roles pagina's kunnen komen.

### API ##
*   RESTful API endpoints zijn beschikbaar (voor bijvoorbeeld integratie met een mobile app).

## Installatie & Setup ##

1.  **Clone de repository**.
2.  **Database Configuratie**:
    *   Controleer de connection string in `appsettings.json` of gebruik User Secrets.
    *   Pas indien nodig de server naam aan (standaard `(localdb)\mssqllocaldb`).
3.  **Starten**:
    *   Open de solution in Visual Studio.
    *   Run het `Manga_Web` project.
    *   De database wordt bij de eerste start automatisch aangemaakt en gevuld met testdata (Seeding).

## Gebruikte Bronnen & Verantwoording ##
### AI Tools ##
*   **GitHub Copilot**:
    *   Ik heb Copilot gebruikt om dummy data te genereren, code te schrijven en uitleg te vragen over bugs.
    *   <img width="600" alt="image" src="https://github.com/user-attachments/assets/2604fc62-ee97-4f99-89ff-7680c636672d" />
*   **ChatGPT**:
    *   Ik heb ChatGPT voornamelijk gebruikt om te debuggen (want Copilot doet dat minder goed) en voor het opzetten van de resources/styling.
    *   [Chat Log 1 - Debugging](https://chatgpt.com/share/690b46f6-9be4-8002-ba82-9edf257524a6)
    *   [Chat Log 2 - Styling](https://chatgpt.com/share/690b4724-d85c-8002-9b4e-a3cbc3135db8)

### Cursusmateriaal ##
*   **Agenda App**
    *   Inspiratie voor de structuur en functionaliteiten van de applicatie heb ik gehaald tijdens de lessen van .Net Frameworks, .Net Advanced en .Net Project. Maar ik heb de code aangepast volgens mijn eigen wensen en project.
