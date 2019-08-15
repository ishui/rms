function parseXml(fileName) {
    try {//Internet Explorer  
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        //加载 XML文档,获取XML文档对象
        xmlDoc.load(fileName);
    } catch (e) {//Firefox, Mozilla, Opera, etc.  
        try {
            xmlDoc = document.implementation.createDocument("", "", null);
            xmlDoc.async = "false";
            //加载 XML文档,获取XML文档对象
            xmlDoc.load(fileName);
        } catch (e) {
            try {//Google Chrome  
                var xmlhttp = new window.XMLHttpRequest();
                xmlhttp.open("get", fileName, false);
                xmlhttp.send(null);
                xmlDoc = xmlhttp.responseXML.documentElement;
            } catch (e) {
                alert("您的浏览器不能正常加载文件。请切换到兼容模式，或者更换浏览器");
            }
        }
    }
    return xmlDoc;
}


function GetXMLResult(url)
{
	//var xmlDoc=new ActiveXObject("MSXML.DOMDocument");
	//xmlDoc.async=false;
 //   xmlDoc.load(url);
    var xmlDoc = parseXml(url);
	var items = xmlDoc.childNodes(0);
	
	return items;
}

function GetXMLTagData(item, tagName)
{
	for(var i=0;i<item.childNodes.length;i++)
	{
		if (item.childNodes(i).tagName.toLowerCase() == tagName.toLowerCase())
		{
			return item.childNodes(i).text;
		}
	}
	
	return "";
}

function EncodeForXML(string)
{
	string=string.replace(/&/g,"&amp;");
	string=string.replace(/</g,"&lt;");
	string=string.replace(/>/g,"&gt;");
	return string;
}

function CommunicateXMLData(sXML,ActionFileURL)
{
    console.log("执行：CommunicateXMLData(sXML,ActionFileURL)");
	//var xmlDoc = new ActiveXObject("MSXML.DOMDocument");
    //xmlDoc.async = false;
    
	sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><TEST>" + sXML + "</TEST>";
    var xmlDoc = parseXml(sXML);

	//var httpObj = new ActiveXObject("Microsoft.XMLHTTP");
    var httpObj = null;
    if (!!window.ActiveXObject || "ActiveXObject" in window) {
        //ie浏览器执行
        httpObj = new ActiveXObject("Microsoft.XMLHTTP");
        httpObj.Open("POST", ActionFileURL, false);
        httpObj.Send(xmlDoc);
        //httpObj.open("POST", "user.do?method=CheckUser&loginid=" + thisForm.loginid.value, false);
        //httpObj.send();
    } else {
        //谷歌浏览器
        httpObj = new XMLHttpRequest("Microsoft.XMLHTTP");
        httpObj.Open("POST", ActionFileURL, false);
        httpObj.Send(xmlDoc);
        //oBao.open("POST", "user.do?method=CheckUser&loginid=" + thisForm.loginid.value, false);
        //oBao.send();
    }
    return httpObj.responseXML.xml;


    //if(xmlDoc.loadXML(sXML))
 //   if (xmlDoc.length == 0)
	//{
 //       return "FALSE";
	//}
	//else
	//{

 //       httpObj.Open("POST", ActionFileURL, false);
 //       httpObj.Send(xmlDoc);
 //       return httpObj.responseXML.xml;
	//}
}
