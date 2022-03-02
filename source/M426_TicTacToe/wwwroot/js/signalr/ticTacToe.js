"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ticTacToe").build();

$(".gameButton").prop("disabled", true);

connection.start().then(function () {
    $(".gameButton").prop("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

for (let i = 1; i <= 9; i++) {
    $("#btn" + i).click(function (event) {
        connection.invoke("ClickField", i).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}

connection.on("UpdateField", function (fieldNumber, fieldContent) {
    $("#btn" + fieldNumber).text(fieldContent);
});