let connection = new signalR.HubConnectionBuilder().withUrl("/MonitoringHub").build();
connection.start();
connection.on("updateDeployList", function (id, name) {
    console.log(id + " " + name);
});