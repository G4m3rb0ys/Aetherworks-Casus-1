# Webapplicatie voor Victuz - Casusopdracht Blok 1

## Inhoudsopgave
1. [Projectbeschrijving](#projectbeschrijving)
2. [Doelstellingen](#doelstellingen)
3. [Technische Specificaties](#technische-specificaties)
4. [Installatie en Configuratie](#installatie-en-configuratie)
5. [Projectstructuur](#projectstructuur)
6. [Samenwerkingsvereisten](#samenwerkingsvereisten)
7. [Prestatie Indicatoren](#prestatie-indicatoren)
8. [Bronnen](#bronnen)

---

## Projectbeschrijving
Dit project betreft de ontwikkeling van een webapplicatie voor **Victuz**, de studievereniging voor ICT-studenten in Zuid-Limburg. De applicatie ondersteunt de organisatie en communicatie binnen de vereniging, inclusief het beheren van activiteiten en het faciliteren van interactie tussen leden.

## Doelstellingen
Het primaire doel van de applicatie is om:
- Leden en geïnteresseerden te informeren over activiteiten en nieuws van de vereniging.
- Een systeem te bieden voor het plannen, aan- en afmelden voor activiteiten.
- Leden in staat te stellen om zelf activiteiten voor te stellen en te organiseren.
- Het bestuur te ondersteunen bij het toevoegen en beheren van activiteiten met verschillende beperkingen en filters.

**Functionaliteiten die in eerste instantie vereist zijn:**
1. Een bestuurslid moet activiteiten kunnen toevoegen en beheren.
2. Leden moeten zich kunnen aanmelden voor en filteren op activiteiten.
3. Leden moeten suggesties voor nieuwe activiteiten kunnen indienen.

**Toekomstige uitbreidingen:**
- Forum en discussieplatform.
- Tutoring functionaliteit.
- Kalenderintegratie en polling voor ledenfeedback.

## Technische Specificaties
- **Framework:** ASP.NET Core (.NET Core) met MVC-patroon
- **Database:** MS SQL, moet voldoen aan het gegeven class diagram.
- **API:** De applicatie bevat een REST API om integratie met andere diensten te faciliteren.
- **Source Control:** Versiebeheer met GitHub om samenwerking en versiebeheer te waarborgen.

## Installatie en Configuratie
### Vereisten
- **.NET Core SDK**: Versie X.XX of hoger
- **SQL Server**: Versie X.X of hoger
- **Git**: Voor versiebeheer en samenwerking

### Stappen om het project lokaal te draaien
1. Clone de repository:
   ```bash
   git clone [repository_url]
   ```
2. Navigeer naar de projectmap en installeer de vereisten.
3. Configureer de database door het SQL-script uit te voeren om de database op te zetten:
   ```sql
   [SQL installatie instructies]
   ```
4. Start de applicatie via Visual Studio of de CLI:
   ```bash
   dotnet run
   ```

## Projectstructuur
- **/Controllers**: Bevat de controllers voor de API en webapplicatie.
- **/Models**: Definieert de datamodellen die worden gebruikt voor database-interacties.
- **/Views**: Bevat de UI-componenten voor het weergeven van pagina's.
- **/wwwroot**: Statistische bestanden zoals CSS, JavaScript en afbeeldingen.
- **/Database**: SQL scripts voor het opzetten van de database.

## Samenwerkingsvereisten
### Scrum-methodiek
- **Sprint Planning**: Het project volgt de Scrum-aanpak, waarbij we in sprints werken. Voorafgaand aan elke sprint wordt het team geïnformeerd over de huidige stand en richting van het project.
- **Versiebeheer**: Wijzigingen worden getraceerd via GitHub om rolverdeling en traceerbaarheid van bijdragen te waarborgen.
- **Voortgangsrapportages**: Wekelijkse updates van voortgang en taken, inclusief een gedeeld samenwerkingscontract en logboeken in GitHub.

## Prestatie Indicatoren
1. **PI6**: Opstellen van een softwareontwerp op basis van requirements en een prototype ontwikkelen.
   - **Product**: Portfolio, inclusief softwareontwerpdocument en gebruikersfeedback.
2. **PI9**: Afspraken nakomen en verantwoordelijkheid delen binnen het team.
   - **Producten**: Samenwerkingscontract, GitHub log, voortgangsrapportages.
   
**Verplichte documentatie voor beoordeling:**
- SRS-document (Software Requirements Specification)
- Ontwerpdossier
- Onderzoeksdocumentatie
- Testrapport
- Database script
- Applicatie met API
- Link naar Source Control (GitHub)
- Demo-video van de applicatie

## Bronnen
- [GitHub Repository](https://github.com/ZuydUniversity/CardgameWar#cardgamewar) - Voor referentie op requirements en diagrammen.
- [Scrum Guidelines](https://scrumguides.org/) - Voor de Scrum-aanpak en methodiek.
