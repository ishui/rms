var OpenedNodeKeys=new Array();

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

function RefreshChildNodes(url,TreeObj,NodeId,Models,keyField,RowClassName,GridClassName){
	var obj=null;
	for(var i=0;i<TreeObj.childNodes.length;i++){
		if(TreeObj.childNodes[i].NodeId==NodeId){
			obj=TreeObj.childNodes[i];
			break;
		}
	}
	
	if(obj==null){
		RemoveAllChildNode(TreeObj);
		GetChildNodes(url,null,Models,keyField,RowClassName,GridClassName);
	}else{
		var shows=obj.getElementsByTagName("div");
		var plusNode=null;
		var minusNode=null;
		var noneNode=null;
		for(var i=0;i<shows.length;i++){
			if(shows[i].id.toLowerCase()=="nodeplus"){
				plusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodeminus"){
				minusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodenone"){
				noneNode=shows[i];
			}
		}
		ClearChildNodes(obj);
		RemoveOpendNodeKeys(obj.NodeId);
		
		var itemCount=GetChildNodes(url+"&Layer="+obj.NodeLayer,obj,Models,keyField,RowClassName,GridClassName);
		if(itemCount){
			InsertOpendNodeKeys(obj.NodeId);
			obj.NodeStatus="Opened";
			plusNode.style.display="none";
			minusNode.style.display="";
			noneNode.style.display="none";
		}else{
			obj.NodeStatus="Closed";
			plusNode.style.display="none";
			minusNode.style.display="none";
			noneNode.style.display="";
		}
	}
}

function RefreshNode(url,TreeObj,NodeId,Models,keyField,RowClassName,GridClassName){
	var obj=null;
	for(var i=0;i<TreeObj.childNodes.length;i++){
		if(TreeObj.childNodes[i].NodeId==NodeId){
			obj=TreeObj.childNodes[i];
			break;
		}
	}
	if(obj==null){
		return;
	}

	//var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
	//xmlDoc.async="false";
	//xmlDoc.load(url);
    var xmlDoc = parseXml(url);
	var Items=xmlDoc.childNodes(1);

	var isShowChildNodes=false;
	var shows=obj.getElementsByTagName("div");
	var plusNode=null;
	var minusNode=null;
	var noneNode=null;
	for(var i=0;i<shows.length;i++){
		if(shows[i].id.toLowerCase()=="nodeplus"){
			plusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodeminus"){
			minusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodenone"){
			noneNode=shows[i];
		}
	}
	if(minusNode.style.display==""){
		isShowChildNodes=true;
	}
	
	for(var i=0;Items!=null&&i<Items.childNodes.length;i++){
		var newObj=GetChildNodeRow(Items.childNodes(i),Models,keyField,RowClassName,GridClassName,isShowChildNodes);
		newObj.NodeIndex=obj.NodeIndex;
		obj.replaceNode(newObj);
	}
}

function RemoveOpendNodeKeys(keyWord){
	var ary=new Array();
	var j=0;
	for(var i=0;i<OpenedNodeKeys.length;i++){
		if(OpenedNodeKeys[i].toString()!=keyWord){
			ary[j]=OpenedNodeKeys[i];
			j++;
		}
	}
	OpenedNodeKeys=ary;
}

function InsertOpendNodeKeys(keyWord){
	var isInsert=true;
	for(var i=0;i<OpenedNodeKeys.length;i++){
		if(OpenedNodeKeys[i].toString()==keyWord){
			isInsert=false;
		}
	}
	if(isInsert){
		OpenedNodeKeys[OpenedNodeKeys.length]=keyWord;
	}
}

function RemoveAllChildNode(obj){
	while(obj.childNodes.length>0){
		obj.removeChild(obj.childNodes[0]);
	}
}

function GetDataSourceUrl(DataSourceUrl,DataSourceUrlParams,node){
	var url=DataSourceUrl;
	for(var i=0;i<DataSourceUrlParams.length;i+=2){
		url+=(i>0?"&":"?");
		url+=DataSourceUrlParams[i];
		url+="=";
		switch(DataSourceUrlParams[i+1]){
			case "#Id":
				url+=node.NodeId;
				break;
			case "#LayerNumber":
				url+=node.NodeIndex.toString().split(".").length.toString();
				break;
		}
	}
	return url;
}

function CollapseNode(node)
{
	var shows=node.getElementsByTagName("div");
	var plusNode=null;
	var minusNode=null;
	var noneNode=null;

	for(var i=0;i<shows.length;i++){
		if(shows[i].id.toLowerCase()=="nodeplus"){
			plusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodeminus"){
			minusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodenone"){
			noneNode=shows[i];
		}
	}

	if(noneNode.style.display!="")
	{
		if(node.NodeStatus=="Opened")
		{
			ClearChildNodes(node);
			node.NodeStatus="Closed";
			plusNode.style.display="";
			minusNode.style.display="none";
			RemoveOpendNodeKeys(node.NodeId);
			return true;
		}
	}
	
	return false;
}

function ExpandNode(url,node,Models,keyField)
{
	var shows=node.getElementsByTagName("div");
	var plusNode=null;
	var minusNode=null;
	var noneNode=null;

	for(var i=0;i<shows.length;i++){
		if(shows[i].id.toLowerCase()=="nodeplus"){
			plusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodeminus"){
			minusNode=shows[i];
		}
		if(shows[i].id.toLowerCase()=="nodenone"){
			noneNode=shows[i];
		}
	}

	if(noneNode.style.display!="")
	{
		if(node.NodeStatus=="Closed")
		{
			GetChildNodes(url,node,Models,keyField,RowClassName,GridClassName);
			node.NodeStatus="Opened";
			plusNode.style.display="none";
			minusNode.style.display="";
			InsertOpendNodeKeys(node.NodeId);
			return true;
		}
	}
	
	return false;
}

function ShowChildNode(url,node,Models,keyField){
	if(node==null){
		GetChildNodes(url,node,Models,keyField);
	}else{
		while(node.NodeName==null||node.NodeName!="_XmlTreeNode"){
			node=node.parentNode;
		}
		var shows=node.getElementsByTagName("div");
		var plusNode=null;
		var minusNode=null;
		var noneNode=null;
		for(var i=0;i<shows.length;i++){
			if(shows[i].id.toLowerCase()=="nodeplus"){
				plusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodeminus"){
				minusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodenone"){
				noneNode=shows[i];
			}
		}
		if(noneNode.style.display!=""){
			if(node.NodeStatus=="Closed"){
				GetChildNodes(url,node,Models,keyField,RowClassName,GridClassName);
				node.NodeStatus="Opened";
				plusNode.style.display="none";
				minusNode.style.display="";
				InsertOpendNodeKeys(node.NodeId);
			}else if(node.NodeStatus=="Opened"){
				ClearChildNodes(node);
				node.NodeStatus="Closed";
				plusNode.style.display="";
				minusNode.style.display="none";
				RemoveOpendNodeKeys(node.NodeId);
			}
		}
	}
}

function ClearChildNodes(node){
	while(node.nextSibling){
		var obj=node.nextSibling;
		if (IsChildNode(obj, node))
		{
			obj.removeNode(true);
			RemoveOpendNodeKeys(obj.NodeId);
		}else{
			break;
		}
	}
}

function GetChildNodes(url,node,Models,keyField,RowClassName,GridClassName){
	//var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
 //   xmlDoc.async = "false";
 //   xmlDoc.load(url);	
    var xmlDoc = parseXml(url);
	//url="http://localhost/RmsPM09/selectbox/systemgroupdata.aspx?classcode=1603";
		
	var Items=xmlDoc.childNodes(1);
	var nextObj;
	if(node==null){
		nextObj=null;
	}else{
		while(node.NodeName==null||node.NodeName!="_XmlTreeNode"){
			node=node.parentNode;
		}
		nextObj=node.nextSibling;
	}
	var i=0;
	for(i=0;Items!=null&&i<Items.childNodes.length;i++){
		var obj=GetChildNodeRow(Items.childNodes(i),Models,keyField,RowClassName,GridClassName,null);
		if(node==null){
			obj.NodeIndex=obj.NodeId;
		}else{
			obj.NodeIndex=node.NodeIndex+"."+obj.NodeId;
		}
		if(nextObj==null){
			TreeObj.appendChild(obj);
		}else{
			TreeObj.insertBefore(obj,nextObj);
		}
	}
	return i;
}

function GetChildNodeRow(item,Models,keyField,RowClassName,GridClassName,isShowChildNodes){
	var obj=document.createElement("TR");
	obj.NodeName="_XmlTreeNode";
	obj.className=RowClassName;
	var NowLayer=0;
	for(var k=0;k<item.childNodes.length;k++){
		//alert("tagName----"+item.childNodes(k).tagName+"  "+"keyField---"+keyField);alert(obj.NodeId);
		if(item.childNodes(k).tagName.toLowerCase()==keyField.toLowerCase()){
			obj.NodeId=item.childNodes(k).text;
		}
		if(item.childNodes(k).tagName.toLowerCase()=="layer"){
			NowLayer=parseInt(item.childNodes(k).text)+1;
		}
	}
	if(NowLayer==0){
		NowLayer=1;//parseInt(LayerNumber);
	}
	obj.NodeLayer=NowLayer;

	for(var m=0;m<Models.length;m++){
		var str=Models[m];
		var IndentStartPoint=str.indexOf("@IndentStart");
		var IndentEndPoint=str.indexOf("@IndentEnd")+"@IndentEnd".length;
		if(IndentStartPoint>-1&&IndentEndPoint>IndentStartPoint){
			var IndentString=str.substring(IndentStartPoint,IndentEndPoint);
			var IndentStr=str.substring(IndentStartPoint+"@IndentStart".length,IndentEndPoint-"@IndentEnd".length);
			var ReplaceString="";
			for(var i=0;i<NowLayer-1;i++){
				ReplaceString+=IndentStr;
			}
			str=str.substring(0,IndentStartPoint)+ReplaceString+str.substring(IndentEndPoint,str.length);
		}
		for(var j=0;j<item.childNodes.length;j++){
			var key=item.childNodes(j).tagName.toLowerCase();
			var value=item.childNodes(j).text.toString();

			var keyPoint=str.toLowerCase().indexOf("@"+key);
			while(keyPoint>-1){
				str=str.substring(0,keyPoint)+value+str.substring(keyPoint+key.length+1,str.length);
				keyPoint=str.toLowerCase().indexOf("@"+key);
			}

			if(key=="childnodescount"){
				var NodeSymbolStartPoint=str.indexOf("@NodeSymbolStart");
				var NodeSymbolEndPoint=str.indexOf("@NodeSymbolEnd")+"@NodeSymbolEnd".length;
				if(NodeSymbolStartPoint>-1&&NodeSymbolEndPoint>NodeSymbolStartPoint){
					var NodeSymbolStr=str.substring(NodeSymbolStartPoint+"@NodeSymbolStart".length,NodeSymbolEndPoint-"@NodeSymbolEnd".length);
					var Symbols=NodeSymbolStr.split("|");
					var ReplaceString="";
					var PlusDisplay="";
					var MinusDisplay="";
					var NoneDisplay="";
					if(parseInt(value)==0){
						PlusDisplay="none";
						MinusDisplay="none";
						NoneDisplay="";
						obj.NodeStatus="Closed";
					}else{

						 if(isShowChildNodes==null){
							if(item.getElementsByTagName("ShowChildNodes")[0].text.toString()=="0"){
								PlusDisplay="";
								MinusDisplay="none";
								NoneDisplay="none";
								obj.NodeStatus="Closed";
							}
							else{
								PlusDisplay="none";
								MinusDisplay="";
								NoneDisplay="none";
								obj.NodeStatus="Opened";
								InsertOpendNodeKeys(obj.NodeId);
							}
						}else{
							if(!isShowChildNodes){
								PlusDisplay="";
								MinusDisplay="none";
								NoneDisplay="none";
								obj.NodeStatus="Closed";
							}else{
								PlusDisplay="none";
								MinusDisplay="";
								NoneDisplay="none";
								obj.NodeStatus="Opened";
								InsertOpendNodeKeys(obj.NodeId);
							}
						}
					}
					ReplaceString+="<div id=\"NodePlus\" style=\"display:"+PlusDisplay+";\">"+Symbols[0]+"</div>";
					ReplaceString+="<div id=\"NodeMinus\" style=\"display:"+MinusDisplay+"\">"+Symbols[1]+"</div>";
					ReplaceString+="<div id=\"NodeNone\" style=\"display:"+NoneDisplay+"\">"+Symbols[2]+"</div>";
					str=str.substring(0,NodeSymbolStartPoint)+ReplaceString+str.substring(NodeSymbolEndPoint,str.length);
				}
			}
		}
		
		var ImageSymbolStartPoint = str.indexOf("@ImageSymbolStart");
		var ImageSymbolEndPoint = str.indexOf("@ImageSymbolEnd") + "@ImageSymbolEnd".length;
		if (ImageSymbolStartPoint>-1 && ImageSymbolEndPoint>ImageSymbolStartPoint)
		{
			var ImageSymbolStr = str.substring(ImageSymbolStartPoint + "@ImageSymbolStart".length,ImageSymbolEndPoint-"@ImageSymbolEnd".length);
			var ImageSymbols = ImageSymbolStr.split("|");
			var ImageReplaceString = "";
			var ImageStatus = item.getElementsByTagName("TaskStatus")[0].text.toString()

			ImageReplaceString =(ImageStatus == "")?"": ImageSymbols[parseInt(ImageStatus)];
			str=str.substring(0,ImageSymbolStartPoint)+ImageReplaceString+str.substring(ImageSymbolEndPoint,str.length);
		}
		
		var ColorSymbolStartPoint = str.indexOf("@ColorSymbolStart");
		var ColorSymbolEndPoint = str.indexOf("@ColorSymbolEnd") + "@ColorSymbolEnd".length;
		if (ColorSymbolStartPoint>-1 && ColorSymbolEndPoint>ColorSymbolStartPoint)
		{
			var ColorSymbolStr = str.substring(ColorSymbolStartPoint + "@ColorSymbolStart".length,ColorSymbolEndPoint-"@ColorSymbolEnd".length);
			var ColorSymbols = ColorSymbolStr.split("|");
			var ColorReplaceString = "";
			var ColorStatus = item.getElementsByTagName("Exceed")[0].text.toString()

			ColorReplaceString = (ColorStatus == "")?"":ColorSymbols[parseInt(ColorStatus)];
			str=str.substring(0,ColorSymbolStartPoint)+ColorReplaceString+str.substring(ColorSymbolEndPoint,str.length);
		}
			
		var ImportantSymbolStartPoint = str.indexOf("@ImportantSymbolStart");
		var ImportantSymbolEndPoint = str.indexOf("@ImportantSymbolEnd") + "@ImportantSymbolEnd".length;
		if (ImportantSymbolStartPoint>-1 && ImportantSymbolEndPoint>ImportantSymbolStartPoint)
		{
			var ImportantSymbolStr = str.substring(ImportantSymbolStartPoint + "@ImportantSymbolStart".length,ImportantSymbolEndPoint-"@ImportantSymbolEnd".length);
			var ImportantSymbols = ImportantSymbolStr.split("|");
			var ImportantReplaceString = "";
			var Important = item.getElementsByTagName("ImportantLevel")[0].text.toString();
			ImportantReplaceString =(Important == "")?"": ImportantSymbols[parseInt(Important)];
			str=str.substring(0,ImportantSymbolStartPoint)+ImportantReplaceString+str.substring(ImportantSymbolEndPoint,str.length);
		}
		
		
		var myRightSymbolStartPoint = str.indexOf("@myRightSymbolStart");
		var myRightSymbolEndPoint = str.indexOf("@myRightSymbolEnd") + "@myRightSymbolEnd".length;
		if (myRightSymbolStartPoint>-1 && myRightSymbolEndPoint>myRightSymbolStartPoint)
		{
			var myRightSymbolStr = str.substring(myRightSymbolStartPoint + "@myRightSymbolStart".length,myRightSymbolEndPoint-"@myRightSymbolEnd".length);
			var myRightSymbols = myRightSymbolStr.split("|");
			var myRightReplaceString = "";
			var myRightStatus = item.getElementsByTagName("IsRight")[0].text.toString()

			myRightReplaceString = (myRightStatus == "")?"":myRightSymbols[parseInt(myRightStatus)];
			str=str.substring(0,myRightSymbolStartPoint)+myRightReplaceString+str.substring(myRightSymbolEndPoint,str.length);
		}
				
				
		var JsCodeStartPoint=str.indexOf("@JsCodeStart");
		var JsCodeEndPoint=str.indexOf("@JsCodeEnd")+"@JsCodeEnd".length;
		if(JsCodeStartPoint>-1&&JsCodeEndPoint>JsCodeStartPoint){
			var JsCodeStr=str.substring(JsCodeStartPoint+"@JsCodeStart".length,JsCodeEndPoint-"@JsCodeEnd".length);
			var ReplaceString=eval(JsCodeStr);
			str=str.substring(0,JsCodeStartPoint)+ReplaceString+str.substring(JsCodeEndPoint,str.length);
		}
		
		var objItem=document.createElement("TD");
		objItem.setAttribute("nowrap","true",0);
		if(m==0){
			objItem.innerHTML=("<a name=\"Node"+obj.NodeId+"Target\"></a>"+str);
		}else{
			objItem.innerHTML=str;
		}
		objItem.className=GridClassName;
		obj.appendChild(objItem);
	}
	return obj;
}



//find node by id, xyq 2004.9
function FindNode(tree, NodeId) {
	var node;
	node = tree.childNodes[0];
	
	if (NodeId == "")
		return null;
		
	while ((node != undefined) && (node != null))
	{
		if (node.NodeId == NodeId)
		{
			return node;
		}

		node = node.nextSibling;
	}
}
	
function SetNodeGridClass(node, name)
{
	if (node)
	{
		if (node.childNodes)
		{
			l = node.childNodes.length;
			for (i=0;i<l;i++)
			{
				node.childNodes[i].className = name;
			}
		}
	}
}

//2004.12
function RemoveNodeAndAllChild(node)
{
	ClearChildNodes(node);
	node.removeNode(true);
	RemoveOpendNodeKeys(node.NodeId);
}

//2005.11
function GetNodeStatus(objNode)
{
		var shows=objNode.getElementsByTagName("div");
		var plusNode=null;
		var minusNode=null;
		var noneNode=null;
		
		for(var i=0;i<shows.length;i++){
			if(shows[i].id.toLowerCase()=="nodeplus"){
				plusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodeminus"){
				minusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodenone"){
				noneNode=shows[i];
			}
		}
		
		if (plusNode.style.display == "") return "Closed";
		if (minusNode.style.display == "") return "Opened";
		if (noneNode.style.display == "") return "";
		
		return "";
}

function CollapseToLayer(TreeObj, Layer)
{
	if (Layer < 0) return;
	if (TreeObj.childNodes.length <= 0) return;	

	var node = TreeObj.childNodes[0];

	while(node)
	{
		if (node.NodeLayer == Layer)
		{
			CollapseNode(node);
		}
		
		node = node.nextSibling;
	}
}

function ExpandToLayer(TreeObj, Layer, url, Models, keyField)
{
	if (TreeObj.childNodes.length <= 0) return;	

	var node = TreeObj.childNodes[0];

	while(node)
	{
		if (node.NodeLayer < Layer)
		{
			ExpandNode(url + "&GetType=ChildNodes&NodeId="+ node.NodeId + "&Layer=" + (node.NodeLayer - 1), node, Models, keyField);
		}
		
		node = node.nextSibling;
	}
}

function GetTreeNode(obj)
{
    var node = obj;
    
	while(node.NodeName==null||node.NodeName!="_XmlTreeNode")
	{
		node=node.parentNode;
	}
	
	return node;
}

function IsChildNode(ChildNode, ParentNode)
{
    if(ChildNode.NodeIndex.length > ParentNode.NodeIndex.length && ChildNode.NodeIndex.substring(0, ParentNode.NodeIndex.length+1)==ParentNode.NodeIndex+".")
        return true;
    else
        return false;
}
