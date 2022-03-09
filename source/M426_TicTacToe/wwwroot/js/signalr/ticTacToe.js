"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ticTacToe").build();

$(".gameButton").prop("disabled", true);

connection.start().then(function () {
    $(".gameButton").prop("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("UpdateField", function (fieldNumber, fieldContent) {
    $("#btn" + fieldNumber).text(fieldContent);
});