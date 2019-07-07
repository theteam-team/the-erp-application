
let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.start();

$(document).ready(
    function () {

        $.getJSON("/api/NotificationApi/GetNotifications", function (Notifications) {
            for (var i = 0; i < Notifications.length; i++) {
                createNotficationCard(Notifications[i]);
            }
            console.log(Notifications);

        });

        connection.on('recieveNotification', function (notification) {
            createNotficationCard(notification)
        });

        function createNotficationCard(notification)
        {
            var el = $('#Notification_Menu');
            if (notification.notificationType == "userTask") {
                el.append("<a  class = 'w3-bar-item w3-button border' href=/UserTask/" +notification.entityID+">" + notification.message + "</a>");

            }

        }
       

    }
)