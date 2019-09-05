# tarentSidler
### azubis 2018-2019 Team
##### Mitglieder
|Name| Role |
|:----|:-----:|
|Serena Bräuer |Developer|
|Julius Marx|Artist|
|Julian Koch|Developer|
|Markus Andreas|Developer|
|Tim Herkenrath|Artist|
|Daniel Lozanu|Developer-Artist|
***
### Beschreibung

> **`tarentSidler`** - Es ist Speil auf tarent GmbH Infrastruktur basiert und mit Sidler Stil/Thema kommbiniert.
>Jeder tarent Abteilung ist pro Gebäude verteilt, um ein tarent Stadt darzustellen.
>Naturlich die Abteilungen kommuniezieren mit einanderen,
>hier ist das so dargestellt das die Mitarbeiter sich bewegen zwischen die Abteilungen um diese Visuele verbindung zu haben,
>dass die Abteilungen nicht selbständig Arbeiten, sondern die sind abhängig von einanderen.
>So wie bei allen anderen Unternehmen gibt es Mitarbeiter, Kunden, Normale menschen , Tiere, Kindern, sowohl als auch Häckereingriefen,
>und diese Faktoren sind in das Spiel dargestellt, um den Blick / der Verbindung zu dem Realität nicht zu verlieren.

>Ziel dieses Spiel ist : 
>den tarent Stadt zu verbessern und im eine armoniesche Arbeitsatmosphere zu bringen.
___
### Regeln
* _Es darf nicht mehr Mitarbeiter von eine Branche als erlaubt ist._
* _Es darf nicht ein Mitarbeiter annehmen wenn kein Projekts gibts._
* _Es darf nicht mehr Mitarbeiter zu einem Projekt mitteilen als erlaubt._

___
### Planung

Mit allen gebäuden etc. **200 Mitbewohner** haben Platz??
<br>***Ressourcen*** = Geld(steuern und ressourcen), Kunde, Projekte
<br>***Gebäude*** (Größe, Anzahl,Arbeitsplätze):

* **tarent Town**(3x3, 1, 10) = Steuern
* **Tom Town**(2x2,1,15) = Projekte
* **Office**(2x3,1,10) = Kunde
* **Rewe Town**(2x2,1, 10) = Projekte
* **Azubis**(2x2,1, 20) = neue Mitarbeiter
* **Büro**(1x1,10, 5 passen in ein Gebäude 5 für jedes Upgrade(4 Möglich)) = Mitbewohner
* **SozialRoom ?**(2x3,1,30)=Loyalität und glückliche arbeiter die die Produktivität erhöhen
* **Park**(3x3,1)=Zufriedenheit und steigert die Produktivität
* **Marketing**(2x3/3x3 kommt aufs Design an ,1,10) = zufluss an ressourcen(minimal) und tausch von Ressourcen, geld zufluss durch verkauf von ressourcen
* **Buchhaltung**(2x2,1,10) = kredite leihen wenn Geld knapp wird ( mit möglichkeit das wöchentlich zinsen abgezogen werden oder du den gesamten Betrag zurückzahlen
* **Dev-Ops**(2x2, 1, 20 inkl platz für 5 gefangene)= sie laufen durch die Stadt und kommen zu Verbrechen
* **Admins**(2x3,1, 10) = ups ein haus brennt, die patrouillierenden Feuerwehrmänner helfen - (“Esel mit 2 Eimer und  ein Mitarbeiter”)
* **Anwalt ? ** (2x2,1,5) =  Anwalt entscheidet über gefängnis
* **Server**(3x3,1, 6) = upgradebar (2 mitarbeiter pro Ressource)
* **Straßen** (1x1)
***
  Feldgröße 13x13 grids / 189 Felder

  bisher benötigte Mitarbeiter = 156 also 200 Mitbewohner


### Klassen-Liste

*  ##### Container 
    Hier befinden sich alle Resourcen was man während des Spiel braucht. Die Resourcen sind im **Awake** Zustand hochgeladen und im Container gespeichert. 
    <br>Die Objecte was die gespawnt wird oder die Materialen und Texturen was währen des Speil generiert wird sollen sich im Container befinden.
*  ##### StateManager :
    Es ist eine generische Klasse was um den Zustand des Spieles sich kümmert.
*  ##### Boot :
    Es ist ein **Singelton**, die alle wichtige und HauptKlassen initialisiert und lässt du verfügugn für alle anderen Klassen.
*  ##### SpawnManager :
    Diese Klasse gibt die Möglichkeit Objekte während des Laufens zu Instanzieren. 
*  ##### CameraMovement :
    Hauptfunktion dieser Klasse ist um die aktuelle Camera im Spiel zu kontrolieren. 
*  ##### AudioManager :
    bittet die möglichkeit in einem bestimmten Zeit mit einem bestiemten Dauer eine bestimmte Music oder Audio effekt zum spielen. 
*  ##### InputManager :
    Es ist ein System / Manager was vereinfacht unsere Kontrolle auf dem Spiel durch dem Schortcuts/ Buttonskombinationen.
*  ##### InputWrapper :
    Seine Aufgabe ist um zu wissen welche Shortscuts benutzt sind und was für eine Action die führen, un man kann die ändern während des Spieles.
*  ##### PathFinder :
    Die **PathFinder** Klasse bietet die Möglichkeit während des Laufens die Objekte zu einem bestiemten Ziel(Position) bewegen lassen, es benötigt ein **Navmesh** und **NavMeshAgent**. 
*  ##### SceneManager :
    Diese Klasse macht den Spring / der Wechselung zwischen den Scenen einfacher, und bittet die möglichkeit zu läzt gewesene Scene zu wechseln. 
*  ##### UIDispatcher :
    Es ist ein Singelton.
    Diese Klasse es kümmert sich um die UI Funktionen / Evenimente.
*  ##### BuildingPackage : 
    * ###### iBuilding: 
        hat die funktionen/Attribute:
        * Price - Struct
        * HitPoints - int
        * Time - Time
        * Upgrade()
    * ###### iAccounting:
        * Work()
        * WorkPlaces
        * Compute()
    * ##### Building : MonoBehaviour
        * Coroutine() ?
*  ##### EntityPackage : 
    * ###### iEntity: 
        hat die funktionen/Attribute:
        * Do() ?
    * ###### iDeveloper:
        * Programming() ?
    * ##### Entity : MonoBehaviour
        * Coroutine() ?
*   ##### CreditPackage
    