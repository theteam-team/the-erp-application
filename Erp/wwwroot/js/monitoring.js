let x = 10;
let y = 20;
function readXml()
{
    
    var xml = new XMLHttpRequest();   
    xml.open('Get', 'http://localhost:8888/js/Design.xml', true);
    xml.send();
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    xml.onload = function ()
    {
        var xmlData = xml.responseText;
        var xmlDoc = new DOMParser().parseFromString(xmlData, "text/xml");
        var startNode = xmlDoc.getElementsByTagName("start")[0];    
        var currentNode = startNode;
        console.log(currentNode.getAttribute('nId') +" "+currentNode.tagName); 
        displayImage(currentNode, x, y, ctx);    
        draw(xmlDoc, currentNode, ctx);
        
    }
}
function draw(xmlDoc, currentNode, ctx)
{
   
    var nextNodeID = currentNode.getElementsByTagName("nextNode");
    for (var i = 0; i < nextNodeID.length; ++i)
    {
        var nextNode = xmlDoc.querySelectorAll("[nId='" + nextNodeID[i].innerHTML + "']")[0];
        x += 100;
        y += 100; 
        displayImage(nextNode, x, y, ctx);
        ///draw the node
        currentNode = nextNode;
        console.log(nextNodeID[i].innerHTML + " " + currentNode.tagName);
        if (currentNode.tagName != "end")
            draw(xmlDoc, currentNode, ctx);
    }

}


function displayImage(currentNode, x, y, ctx)
{
    var img = new Image();
    img.onload = function () {
        ctx.drawImage(img, x, y)
    }
    img.src = 'http://localhost:8888/img/nodes/' + currentNode.tagName + '.png';
}