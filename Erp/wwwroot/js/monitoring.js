let WorkFlowObjext =
{
    "Nodes": {},
    "Variables": {},
    "Position": {}
    
};
let loaded = false;
let xmlns = "http://www.w3.org/2000/svg";
let xlinkns = "http://www.w3.org/1999/xlink";
let WorkFlow = document.getElementById("WorkFlow");
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

function readXml() {
    if (!loaded) {
        var xml = new XMLHttpRequest();
        xml.open('Get', 'http://localhost:8888/js/Design.xml', true);
        xml.send();
        
        xml.onload = function () {
            var xmlData = xml.responseText;
            var xmlWorkFlow = new DOMParser().parseFromString(xmlData, "text/xml");
            var startNode = xmlWorkFlow.getElementsByTagName("start")[0];
            var currentNode = startNode;
            console.log(currentNode.getAttribute('nId') + " " + currentNode.tagName);

            drawImage(currentNode, xmlWorkFlow);

            Parse(xmlWorkFlow, currentNode);

            loaded = true;

        }
    }
}
function Parse(xmlWorkFlow, currentNode)
{
   
    var nextNodeID = currentNode.getElementsByTagName("nextNode");
    for (var i = 0; i < nextNodeID.length; ++i)
    {
        var nextNode = xmlWorkFlow.querySelectorAll("[nId='" + nextNodeID[i].innerHTML + "']")[0];     
        drawImage(nextNode, xmlWorkFlow);
        currentNode = nextNode;
        console.log(nextNodeID[i].innerHTML + " " + currentNode.tagName);
        if (currentNode.tagName != "end")
            Parse(xmlWorkFlow, currentNode);
    }
    
}


function drawImage(currentNode, xmlDoc) {
    var imgSrc = 'http://localhost:8888/img/nodes/' + currentNode.tagName + '.png';
    var node = WorkFlowObjext.Nodes;
    var img = document.createElementNS(xmlns, 'image');
    currentNodeId = currentNode.getAttribute("nId");
    img.setAttribute('height', '100');
    img.setAttribute('width', '100');
    img.setAttribute ('id', currentNodeId);
    img.setAttribute ('nodeName', currentNode.tagName);
    img.setAttributeNS(xlinkns, "href", imgSrc);
    Type = currentNode.getAttribute("type");
    node[currentNodeId] = {
        "img": undefined, "name": currentNode.tagName
        , "type": Type, "x": 0, "y": 0
    };
    getPosition(xmlDoc, currentNode);
    posX = node[currentNodeId].x;
    posY = node[currentNodeId].y;
    img.setAttribute("x", node[currentNodeId].x);
    img.setAttribute("y", node[currentNodeId].y);
    node[currentNodeId].img = img;
    
    WorkFlow.appendChild(img);
    
    
}

function getPosition(xmlDoc , currentNode)
{
    currentNodeId = currentNode.getAttribute("nId");
    var NodePosition = xmlDoc.getElementsByTagName("positions")[0]
        .querySelectorAll("[nId='" + currentNodeId + "']")[0];
    var x = NodePosition.getElementsByTagName("x")[0].innerHTML;
    var y = NodePosition.getElementsByTagName("y")[0].innerHTML;
    WorkFlowObjext.Nodes[currentNodeId].x = x;
    WorkFlowObjext.Nodes[currentNodeId].y = y;
        
}



function change(NodeId)
{
   
    if (loaded)
    {

        var img = $("#"+NodeId);
        console.log(img);
        imgSrc = 'http://localhost:8888/img/nodes/' + img.attr("nodeName")+ "_chosen" + '.png';
        img.attr("href", imgSrc);
    }
}