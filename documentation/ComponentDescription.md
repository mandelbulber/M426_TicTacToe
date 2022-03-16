# Kommunikation zwischen den Komponenten

## SignalR

SignalR ist eine Library, welche mit C# und JavaScript funktioniert. Es erlaubt, dass der Server den Clients Nachrichten sendent.

Im TicTacToe wird SignalR hauptsächlich verwendet, um das TicTacToe-Feld zu aktualisieren.
Der Client schickt ruft eine SignalR-Funktion auf, welche in einem Hub (Verbindungsknoten von SignalR) aufgerufen wird. In ihr wird über die Datenbank (Entity Framework) das Spiel ausgelesen, die Eingabe überprüft und das Feld aktualisiert. Danach wird jedem Client, der im Spiel eingetragen ist, eine Nachricht gesendet, welche eine JavaScript-Funktion auf dem Browser ausführt. Schlussendlich wird in der Funktion das Feld live aktualisiert.

SignalR wird ebenfalls bei einem Spielende angezeigt. Hat jemand gewonnen, oder es gab ein Unentschieden, wird ebenfalls eine Funktion ausgelöst, welche dies anzeigt.

## Entity Framework

Die Controller und der SignalR-Hub benötigen eine Datenbank. Unsere Applikation ist für eine MSSQL-Datenbank eingerichtet. Um über dem Controller und dem Hub mit der Datenbank zu kommunizieren, wird Entity Framework benutzt.

## MVC

Die Model - View - Controller Architektur kommuniziert mit Client, und Entity Framework.

Anfragen des Clients werden vom Controller aufgenommen, werden mit Entity Framework, den Views und Models verarbeitet und zurückgegeben.



![](/ComponentDiagram.png)