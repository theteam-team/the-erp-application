$(document).ready(
    function () {
        let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
        $.getJSON("/api/NotificationApi/GetNotifications", function (Notifications) {
            for (var i = 0; i < Notifications.length; i++) {
                createNotficationCard(Notifications[i]);
            }

        });
        var el = document.getElementById('Notification_Menu');
        
        el.append('<a href="#" class="w3-bar-item w3-button border">Link 14</a>');
        function createNotficationCard()
        {

        }
        //el.append()

    }
)