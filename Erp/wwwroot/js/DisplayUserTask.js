
let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
var el = $('#UserTaskHolder').attr('name')
connection.start().then(() => { connection.send('AddToGroup',el) });
let userTasks = [];
$(document).ready(
    function () {
        var el = $('#UserTaskHolder').attr('name')
        $.getJSON("/api/ServicesApi/getUserTask/"+ el, function (Notifications) {

            if (Notifications)
                createNotficationCard(Notifications);
            else
                createIsDone();

        });
        connection.on('removeUserTask', function (userTaskId) {
            console.log("hereee");

            var txt = '#' + userTaskId;
            var el = $(txt);
            el.hide(400);
            el.remove();
            createIsDone();
        });
        function createIsDone()
        {

            var not = $('#UserTaskHolder');

           
            var e = document.createElement('div');
            var el = $(e);
          
            el.attr('class', 'col-md-6 offset-md-3 card border border-primary shadow-lg p-3 mb-5 bg-white rounded');
            el.attr('style', 'width:Auto;float:left;margin:10px');
            not.append(el);
            var req = document.createElement('div');
            $(req).attr('class', 'card-body');
            var title = document.createElement('div');
            $(title).attr('class', 'form-group');

            $(title).append('<p class="red-text text-center">' + " User Task is Finished" + '</p>')
            $(req).append($(title));
            el.append($(req));
        }

        function createNotficationCard(notification)
        {
            userTasks.push(notification);

            var not = $('#UserTaskHolder');
            var userTaskId = notification.id;
            var e = document.createElement('div');
            var el = $(e);
            el.attr('id', userTaskId);
            el.attr('class', 'col-md-6 offset-md-3 card border border-primary shadow-lg p-3 mb-5 bg-white rounded');
            el.attr('style', 'width:Auto;float:left;margin:10px');
            not.append(el);

            var req = document.createElement('div');
            $(req).attr('class', 'card-body');
            var title = document.createElement('div');
            $(title).attr('class', 'form-group');

            $(title).append('<p class="red-text text-center">' + notification.title + '</p>')
            $(req).append($(title));
            var parameters = notification.userTaskParameters;
            for (var i = 0; i < parameters.length; i++) {
                createResponseChoice(parameters[i].name, parameters[i].type, userTaskId, $(req))
            }

            el.append($(req));

        }
        function createResponseChoice(name, type, userTaskId, JqueryEl) {
            var n = name.replace(/\s/g, '');
            var t = type.replace(/\s/g, '');
            var id = userTaskId + "" + n + "" + type;
            var el = document.createElement('div');
            $(el).attr('class', 'form-group');
            if (type == "textField") {

                var lbl = document.createElement('label');
                $(lbl).attr("for", id);
                $(lbl).append(name + "");
                var txt = document.createElement('input');
                $(txt).attr('type', 'text');
                $(txt).attr("class", "form-control");
                $(txt).attr('name', name);
                $(txt).attr('id', id);
                $(el).append((lbl), (txt));
            }
            else if (type == "button") {
                var btn = document.createElement('input');
                $(btn).attr('type', 'button');
                $(btn).attr('value', name);
                $(btn).attr("class", " btn btn-primary form-control");
                $(btn).attr('id', id);
                $(btn).on('click', function () { notificationResponse(userTaskId, id); });
                $(el).append((btn));

            }
            JqueryEl.append((el));


        }

        function notificationResponse(userTaskId, btnId) {
            var userTaskResponse =
            {
                id: "",
                userTaskParameters: []
            };
            var txt = '#' + userTaskId;
            var el = $(txt);
            var uTs = userTasks.map(x => {
                if (x.id == userTaskId)
                    return x;
            });
            console.log(uTs);
            var userTask;
            for (var i = 0; i < uTs.length; i++) {
                if (!uTs[i])
                    continue;
                userTask = uTs[i];
                break;

            }
            console.log(userTask);

            var paramters = userTask.userTaskParameters;
            userTaskResponse.id = userTaskId;
            for (var i = 0; i < paramters.length; i++) {
                var name = paramters[i].name;
                var type = paramters[i].type;
                var n = name.replace(/\s/g, '');
                var t = type.replace(/\s/g, '');

                var paramId = userTaskId + "" + n + "" + t;
                var param = $('#' + paramId);
                if (type == "textField") {
                    console.log(param.val());
                    console.log(param);
                    userTaskResponse.userTaskParameters.push(
                        {
                            "name": name,
                            "type": type,
                            "value": param.val()
                        });
                }
                else if (btnId == paramId) {

                    userTaskResponse.userTaskParameters.push(
                        {
                            "name": name,
                            "type": type,
                            "value": "true"
                        });
                }
                else {
                    userTaskResponse.userTaskParameters.push(
                        {
                            "name": name,
                            "type": type,
                            "value": null
                        });
                }
            }
            var res = JSON.stringify(userTaskResponse);
            console.log(userTaskResponse);
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/api/ServicesApi/RespondToUserTask",
                dataType: "json",
                data: res,
                success: function (response) {
                    console.log(response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(jqXHR.responseJSON);;
                }
            }).done(function (result) { console.log(response) });

            //el.hide(400);
            //el.remove();
            //createIsDone();
            connection.send('removeUserTask', userTaskId);
        }
        
    }

   
)