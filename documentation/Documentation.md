# M426 TicTacToe Dokumentation

[TOC]

## Einleitung

Der Sinn der folgenden Dokumentation liegt darin, Dinge aufzuzeigen, welche wir gemacht haben, wie wir gewisse Dinge gemacht haben und sonstige Sachen.
Diese Dokumentation wurde nicht im Detail geführt, da Anforderungen dieser Dokumentation in keinerlei schriftlicher Form dokumentiert wurden. Es ist undefiniert, was, wie, in welchem Umfang und wie detailliert in dieser Dokumentation dokumentiert werden soll. In dem Dokument "M426-Projektarbeit Bewertung.docx" wird nirgends definiert, dass diese Dokumentation in die Bewertung einfliesst, weswegen die Detaillierung geringer ausfallen kann, als die "Erwartungen" sind.

Unsere Annahme besteht darin, aufzuzeigen, was ungefähr wie gemacht wurde.

## Projektbeschreibung

- Online TicTacToe, welches live aktualisiert wird
- History, welche aufzeigt, welche Spiele am laufen sind, welche fertig sind
- Leaderboards um zu schauen, wer wie viel gewonnen/verloren hat
- Offenen Spielen kann einfach beigetreten werden (mit Link)
- Authentifizierung
- Spiele können pausiert werden
- Btw: UI ist je nach Systemeinstellungen dunkel oder hell

## Scrum

### Rollen

- Product Owner: Andrin Eugster
- Scrum Master: Nick Schürmann
- Developer: Alle

### Daily Scrum

- Wurde wöchentlich (Mittwoch) durchgeführt gemäss Definition des Dailys.
- Wurde zu Beginn des Tages durchgeführt.

### Sprint Review

- Wurde wöchentlich (Mittwoch) durchgeführt gemäss Definition von einem Sprint Review.
- Wurde im Anschluss des Dailys durchgeführt.

### Sprint Retrospective

- Wurde wöchentlich (Mittwoch) durchgeführt gemäss Definition einer Sprint Retrospective.
- Wurde im Anschluss des Sprint Reviews durchgeführt.

### Sprint Plannings

- Wurde wöchentlich (Mittwoch) durchgeführt gemäss Definition eines Sprint Plannings.
- Wurde im Anschluss der Sprint Retrospective durchgeführt.

### Planning Poker

- Wurde bei jedem Task angewendet
- Unter anderem wurde https://planningpokeronline.com/ benutzt.

### Scrum Artefakte

- Product Backlog ist hier zu finden: https://reallyhandsomepeople.atlassian.net/jira/software/projects/M426/boards/1/backlog
- Sprints sind hier zu finden: https://reallyhandsomepeople.atlassian.net/jira/software/projects/M426/boards/1/reports/burndown
- Increments (Releases) sind hier zu finden: https://github.com/mandelbulber/M426_TicTacToe/releases

## Retrospektive

- Anlaufszeit
  - Zu Beginn Abhängigkeiten zwischen Tasks
  - Zu Beginn Story Points falsch eingeschätzt
- Uneinigkeiten (DB)
  - Zeit verschwendet, das Datenbankmodell zu definieren (uneinigkeiten untereinander)
- Sinnvolle Aufteilung
  - Tasks
    - Wurden sinnvoll aufgeteilt, je nach Fachwissen
    - Passende Grösse (Story Points) für nicht zu kleine, aber auch nicht zu grosse Tasks
  - Team
    - Rollenverteilung wurde gut eingehalten
    - Aufgaben wurden gelichmässig aufgeteilt
- Kommunikation
  - Jeder wusste, woran der Andere am Arbeiten ist
  - Jeder wusste gewissermassen, wie welche Technologien funktionieren (Trainings untereinander)
  - Bei schwierigkeiten wurden das Problem im Team besprochen
  - Passendes Team

## Source Code

Der Source Code ist im dementsprechendem Ordner (source) zu finden (Repository/source). Ebenfalls ist das Repository in GitHub: https://github.com/mandelbulber/M426_TicTacToe

## Benutzte Technologien & Tools

- .NET 5.0

  - EntityFramework

  - ASP (MVC)

- MSSQL

- SignalR

- Docker

- GitHub

- Jira

## CI/CD

- CI
  - Ist eingerichtet
  - Werden bei Commit gebuilded und Test werden durchgeführt
  - Durchgeführte Builds und Test-Runs sind hier zu finden: https://github.com/mandelbulber/M426_TicTacToe/actions
- CD
  - Ist nicht eingerichtet
  - Docker ist jedoch eingerichtet, um die WebApp zu deployen

## Tests

### Unit Tests

Alle alleinstehende Methoden, bei denen es Sinn ergibt, diese zu testen, wurden mit Unit Tests abgedeckt. Private Methoden wurden nicht beachtet.

Die Code-Coverage, welche vom Visual Studio berechnet wird ist in unserem Fall recht niedrig. Dies liegt daran, dass Dinge beachtet werden, welche wir nicht testen können, oder es einfach keinen Sinn ergibt, diese zu testen.
Somit sollte die Code-Coverage von unserem Code auf fast 100% liegen.

### Exploratory Testing

Bei Änderungen wurden die beeinflussten Bereiche nach ihrer Funktion getestet.

Feedback von Umstehenden wurde ebenfalls aufgenommen.

Ab und zu wurde die Software beim TicTacToe spielen auf Funktionalität und Konform getestet.

### Testfälle

Vor dem zweitem Release v.1.0.1 wurden Testfälle durchgeführt.

Das Dokument ist hier zu finden: [TestCases.docx](TestCases.docx)

## Weitere Dokumentation

Weitere Dokumentationen sind im selben Ordner zu finden, in dem diese Dokumentation abgelegt ist. (Repository/documentation)

- [ComponentDescription.md](ComponentDescription.md)
- [ComponentDiagram.drawio](ComponentDiagram.drawio) 
- [ClassDiagram.drawio](ClassDiagram.drawio) 
- [Presentation.pptx](Presentation.pptx) 