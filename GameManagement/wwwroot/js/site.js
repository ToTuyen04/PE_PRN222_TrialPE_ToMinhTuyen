"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalrhub")
    .build();

connection.on("LoadData", function () {
    location.href = "/Games/Index";
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

