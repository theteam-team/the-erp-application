$(document).ready(
    function ()
    {
        $.getJSON("/api/NotificationApi/GetUnResponsedNotifications", function (Notifications) {            
            for (var i = 0; i < Notifications.length; i++) {
                createNotficationCard(Notifications[i]);
            }
            console.log(Notifications);
        });
        
    }
)
let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.start().then(() => { connection.send('AddToGroupRole') });

connection.on('receiveNotification', function (notification) {
    console.log(notification);
    createNotficationCard(notification)
});


function createNotficationCard(notification)
{
    var not = $('#NodtficationHolder');
  
    var e = document.createElement('div');
    var el = $(e);
    el.attr('id', notification.id);
    el.attr('class', 'card border border-primary shadow-lg p-3 mb-5 bg-white rounded');
    el.attr('style', 'width:300px;float:left;margin:5px');   
    not.append(el);

    var req = document.createElement('div');   
    
    $(req).attr('class', 'card-body');
    $(req).append('<h4 class="card - title">' + notification.notificationType+'</h4>')
    $(req).append('<p class="card-text">' +notification.message+'</p>')
    var responses = notification.notificationResponses;
    for (var i = 0; i < responses.length; i++) {
        createResponseChoice(responses[i].response, $(req))
    }
    var subbtn = document.createElement('input');
    $(subbtn).attr('type', 'button');
    $(subbtn).attr('class', 'btn btn-primary');
    $(subbtn).attr('value', "Submit");
    $(subbtn).on('click', function () { notificationResponse(notification.id); })
    
    $(req).append($(subbtn));

    el.append($(req));

}

function createResponseChoice(value, JqueryEl)
{
    var lbl = document.createElement('label');
    var btn = document.createElement('input');
    $(btn).attr('type', 'radio');
    $(btn).attr('name', 'Response');
    $(btn).attr('value', value);
    $(lbl).append($(btn));
    $(lbl).append(" <strong>" +value+"<strong>");
    JqueryEl.append((lbl), '<br>');
}

function notificationResponse(notificationId)
{
    var txt = '#' + notificationId;  
    var el = $(txt);
    var radioValue = el.find("input[name='Response']:checked").val();
    if (radioValue) {
        connection.send('notificationResponse', notificationId, radioValue);
        el.toggle(400);
        el.remove();
    }
}
connection.on('removeNotification', function (notificationId) {
    
    var txt = '#' + notificationId;
    var el = $(txt);
    el.hide(400);
    el.remove();
    
});
