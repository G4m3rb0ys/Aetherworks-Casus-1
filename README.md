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
- **.NET Core SDK**: Versie 8.0 of hoger
- **SQL Server**: Versie 8.0.8 of hoger
- **Git**: Voor versiebeheer en samenwerking

### Stappen om het project lokaal te draaien
1. Clone de repository:
   ```bash
   git clone https://github.com/G4m3rb0ys/Aetherworks-Casus-1
