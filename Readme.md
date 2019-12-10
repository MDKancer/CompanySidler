# tarentSidler
### azubis 2018-2019 Team
##### Mitglieder
|Name| Role | Progress |
|:----|:-----:|:-----:|
|Serena Bräuer|Developer|Idee|
|Julian Koch|Developer|10%|
|Tim Herkenrath|Artist|-|
|Daniel Lozanu|Developer-Artist|-|
***
### Beschreibung

> **`tarentSidler`** - Es ist Spiel auf tarent GmbH Infrastruktur basiert und mit Sidler Stil/Thema kommbiniert.
>Jeder tarent Abteilung ist pro Gebäude verteilt, um ein tarent Unternehmen darzustellen.
>Naturlich die Abteilungen kommuniezieren mit einanderen,
>hier ist das so dargestellt das die Mitarbeiter sich bewegen zwischen die Abteilungen um diese Visuele verbindung zu haben,
>dass die Abteilungen nicht selbständig Arbeiten, sondern die sind abhängig von einanderen.
>So wie bei allen anderen Unternehmen gibt es Mitarbeiter, Kunden, Normale menschen , Tiere, Kindern, sowohl als auch Häckereingriefen,
>und diese Faktoren sind in das Spiel dargestellt, um den Blick / der Verbindung zu dem Realität nicht zu verlieren.

>Ziel dieses Spiel ist : 
>den tarent Firma zu verbessern und zu einer gute Arbeitsatmosphere zu bringen.
___
### Regeln
* _Es darf nicht mehr Mitarbeiter von eine Branche als erlaubt ist._
* _Es darf nicht ein Mitarbeiter annehmen wenn kein Projekts gibts._
* _Es darf aber die Mitarbeiter arbeiter ohne Projekt.
<br>(Zwischen 2 Projekte , anfange und ende wird eine Minimale Pause,<br> da können die Mitarbeiter arbeiten ohne Projekt).
* _Es darf nicht mehr Mitarbeiter zu einem Projekt mitteilen als erlaubt._

___
### Planung

Mit allen gebäuden etc. **200 Mitbewohner** haben Platz??
<br>***Ressourcen*** = Geld(steuern und ressourcen), Kunde, Projekte
<br>***Gebäude*** (Größe, Anzahl,Arbeitsplätze):

* **tarent Town**(3x3,) = Controlling
* **Tom Town**(2x2) = Projekte
* **Office**(2x3) = Kunde
* **Rewe Town**(2x2) = Projekte
* **Azubis**(2x2) = neue Mitarbeiter
* **Büro**(1x1) = Mitbewohner
* **SozialRoom**(2x3) = Loyalität und glückliche arbeiter die die Produktivität erhöhen, die Arbeiter können hier eine Pause machen.
* **Park ?**(3x3)=Zufriedenheit und steigert die Produktivität
* **Marketing**(2x3/3x3 kommt aufs Design an) = zufluss an ressourcen(minimal)Produkte Designen, Vorstellen, Verkaufen.
* **Buchhaltung**(2x2) = geld zufluss durch verkauf von ressourcen.
* **Dev-Ops**(2x2)= sie reparieren / hosten / produzieren neue Produkte, und im Fall wenn die Sicherheit eines Gebäude runter ist, versucht die hoch zu bringen. 
* **Admins**(2x3) = Produkte hosten / Produziere / , Servers hosten, unter kontrolle haben etc.
* **Telekom** (2x2) = Projekte  
* **Server**(3x3) = server zum mieten bieten.
* **Floors** (1x1)
***
  Feldgröße 13x13 grids / 189 Felder

  bisher benötigte Mitarbeiter = 156 also 200 Mitarbeiter


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
    Diese Klasse es kümmert sich um die UI Funktionen / Events.
*  ##### BuildingPackage : 
    > ein Gebäude kann **viele** oder **keine Mitarbeitern** behalten, aber kann **viele Gäste / Besucher** haben.
    es kann **nur ein Kunde / Customer / Client** haben. 
    <br>
    **1 Customer = 1 Project**
    <br>
    ein Gebäude muss allem seinern Mitarbeitern Lohn geben / bezahlen.
    
    * Accounting
    * Office
    * SocialRoom
    * TomTown
    * ReweTown
    * Server
    * Azubis
    * Administration
    * TarentTown
    * DevOps
    * Marketing
    <br>
    <br>**Besondere Einheit**
    * Hacker
*  ##### EntityPackage : 
    > Ein Mitarbeiter kann nur zu einem Gebäude gehören, und kann andere Gebäude besuchen.
     Ein Mitarbeiter verdient geld vom Gebäude, egal ob ein Projekt gibt's.
     Ein Mitarbeiter soll die Aufgabe des Projekt erlediegen, und übernimmt eine andere Aufgabe (wenn gibt's) wenn er seiner aufgabe erledigt hat.
     
    * Admin
    * Accounter
    * Azubi
    * Tester
    * Analyst
    * Designer
    * Developer
    * DevOps
    * TeamLeader
    * ProductOwner
    * Personal
  #### Client / Customer (Kunde)
  >Die Kunden kommen zu Hauptgebäude mit ihren Projekten. und die Hauptgebäude verteilt diese Projekten an anderen gebäude.
  * Jede Gebäude kriegt 3 mögliche aber verschidene Projekten.
  * Wenn das Projekt nicht erledigt ist, der Kunde verliert seine Interressen an unsere Firma.
  * Ein Kunde kann nur ein Projekt anbieten.
  * Der Kunde kann ein anderen Projekt anbieten , wenn sein aktuellen Projekt erledigt ist. 
*   ##### CreditPackage
    * Es ist eine UI-darstellung / Präsentation , wer hat ein an Project mitgearbeiten.
*  ##### ProjectPackage
    * Jedes Gebäude darf nur ein Project haben, um Geld zu verdienen. 
    * Wenn ein Gebäude kein Projekt hat aber Mitarbeitern hat, dann die Abteilung / das Gebäude verliert Geld pro Mitarbeiter.
    * Jedes Projekt Hat ein **verschidenes Budget**, und auch **verschidene Aufgaben** zu erledigen.
    * Vom Budge werden die Mitarbeiter ihren Lohn bekommen.
    * Jedes Project hat eine **verschiedene Dauerzeit**.
    * Wenn **die Aufgaben nicht erledigt** sind und das **Budget** schon **geleert** ist,
     <br> die **Firma** kriegt eine **Strafe (10% vom Budget)**, und die Firma verliert den Kunde.
    * Jeder Aufgabe benötigt eine bestiemte Zeit zum erledigen.
    * Jede Aufgabe hat ein Progress-Zustand.
    * Jede Aufgabe hat ein Aufgabenträger.