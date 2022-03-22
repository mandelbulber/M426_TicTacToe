"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ticTacToe").build();

$(".gameButton").prop("disabled", true);

connection.start().then(function () {
    $(".gameButton").prop("disabled", false);
    connection.invoke("IsUserPlayer2", $("#gameId").val());
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("UpdatePlayer2", function (name) {
    $("#player2").text(name + " (O)");
});

connection.on("UpdateField", function (fieldNumber, fieldContent) {
    $("#btn" + fieldNumber).text(fieldContent);
    if ($('#gameState').text() == "X's turn") {
        $('#gameState').text("O's turn")

    } else {
        $('#gameState').text("X's turn")

    }
});

connection.on("EndGame", function (winnerState, winnerName) {
    if (winnerState === 1) {
        $("#gameState").text(winnerName + " (X) has won the game");
    } else if (winnerState === 2) {
        $("#gameState").text(winnerName + " (O) has won the game");
    }
    else {
        $("#gameState").text("Draw");
    }
});