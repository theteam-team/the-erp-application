

let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.start().then(() => { connection.send('AddToGroupRole') });

connection.on('receiveNotification', function (notificationId, message) {
    createNotficationCard(notificationId, message)
});



function createNotficationCard(notificationId, message)
{
    var not = $('#NodtficationHolder');
  
    var e = document.createElement('div');
    var el = $(e);
    el.attr('id', notificationId);
    el.attr('class', 'card border border-primary shadow-lg p-3 mb-5 bg-white rounded');
    el.attr('style', 'width:300px;float:left;margin:5px');   
    not.append(el);

    var req = document.createElement('div');   
    
    $(req).attr('class', 'card-body');
    $(req).append('<h4 class="card-title">Request</h4>')
    $(req).append('<p class="card-text">'+message+'</p>')
   
    var lbl1 = document.createElement('label');
    var ysbtn = document.createElement('input');
    $(ysbtn).attr('type', 'radio');
    $(ysbtn).attr('name', 'yes_no');
    $(ysbtn).attr('value', 'yes');
    $(lbl1).append($(ysbtn));
    $(lbl1).append(" Approve");

    var lbl2 = document.createElement('label');
    var nobtn = document.createElement('input');
    $(nobtn).attr('type', 'radio');
    $(nobtn).attr('name', 'yes_no');
    $(nobtn).attr('value', 'no');
    $(lbl2).append($(nobtn));
    $(lbl2).append(" Disapprove");

    var subbtn = document.createElement('input');
    $(subbtn).attr('type', 'button');
    $(subbtn).attr('class', 'btn btn-primary');
    $(subbtn).attr('value', "Submit");
    $(subbtn).on('click', function()  { notificationResponse(notificationId); })
    
    $(req).append($(lbl1), '<br>',$(lbl2), '<br>',$(subbtn));

    el.append($(req));

}


function notificationResponse(notificationId)
{
    var txt = '#' + notificationId;  
    var el = $(txt);
    var radioValue = el.find("input[name='yes_no']:checked").val();
    var response = false;
    radioValue == "no" ? response = false : response = true;
    connection.send('notificationResponse', notificationId, response);
    el.toggle(400);
}