$(document).ready(
    function () {
        let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
        $.getJSON("/api/NotificationApi/GetNotifications", function (Notifications) {
            for (var i = 0; i < Notifications.length; i++) {
                createNotficationCard(Notifications[i]);
            }

        });
        var el = $('#' + 'Notification_Menu');
        function createNotficationCard()
        {

        }
        //el.append()

    }
)