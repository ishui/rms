<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectPerson" CodeFile="SelectPerson.aspx.cs" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
		<title>选择用户</title>
		<link href="../Images/index.css" type="text/css" rel="stylesheet"/>
		<link href="../Images/TreeView.css" type="text/css" rel="stylesheet"/>
		<script type="text/javascript" language="javascript" src="../Images/XMLTree.js"></script>
		<script type="text/javascript" language="javascript" src="../Images/ContentMenu.js"></script>
		<script type="text/javascript" language="javascript" src="../Rms.js"></script>
		<style type="text/css">
		.default
        {
	        border: solid 0px #90B7D4;
	        border-bottom-color:#90B7D4;
	        border-bottom-width:0px;
	        background-color:#EAEAEA;
	        font-size:9pt;
	        font-family:宋体;
	        font-weight:bolder;
	        color:Gray;
        }
        .over
        {
	        margin-left:1px;
	        margin-right:1px;
	        background:#90B7D4;
	        border:solid 1px #90B7D4;
	        border-bottom-width:0px;
	        vertical-align:bottom;
	        color:White;
	        cursor:hand;
	        filter:progid:DXImageTransform.Microsoft.Gradient(startColorStr='#A4C9E4', endColorStr='#85ADCB', gradientType='0');
	        vertical-align:middle;	    
        }
		.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }
	A { COLOR: #000000; TEXT-DECORATION: none }
	A:hover { TEXT-DECORATION: none }
		</style>
		<script language="javascript" type="text/javascript">
    <!-- Hide 
function killErrors() { 
return true; 
} 
window.onerror = killErrors; 
// --> 
	    var clicktd;
	    var clicknode;
    	
	    function TDClick(obj) {
		    if (clicktd != undefined) {
			    clicktd.className='';
		    }
		    obj.className='LeftMenuItemOnMouseOver';
		    clicktd = obj;
	    }
    		
	    function DecodeId(id) {
		    if (id == "")
			    return id;
    			
		    var i = id.indexOf("_");
		    if (i < 0)
			    return id;
    			
		    return id.substr(i+1, id.length - i - 1);
	    }
    	
	    function GetNodeType(id) {
		    if (id == "")
			    return "";
    			
		    return id.substr(0, 1);
	    }
    		


	    //按id查找节点
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
    	
	    function RemoveLast(s, sep) 
	    {
		    var i = s.lastIndexOf(sep);
		    if (i > -1) 
		    {
			    return s.substr(0, i);
		    }
		    else
			    return s;
	    }

	    function GetLast(s, sep) 
	    {
		    var i = s.lastIndexOf(sep);
		    if (i > -1) 
		    {
			    return s.substr(i + 1, s.length - i - 1);
		    }
		    else
			    return "";
	    }

	    function GetParentNodeId(node) {
		    if ((node == undefined) || (node == null))
			    return "";
    			
		    var s = node.NodeIndex;
		    var i;
		    var stemp;
    		
		    stemp = RemoveLast(s, ".");
		    if (stemp == s)
			    return "";
    			
		    s = stemp;
		    stemp = GetLast(s, ".");
		    if (stemp == "")
			    return s;
		    else
			    return stemp;
	    }

    </script>
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    选择用户</td>
            </tr>
            <tr>
                <td height="100%">
                    <table cellspacing="0" cellpadding="0" border="1" height="100%" width="100%">
                        <tr>
                            <td width="40%">
                                <div style="overflow: auto; width: 100%; height: 100%">
                                    <div runat="server" visible="false" id="titlehead" class="default" style="text-align: center; width: 100%;
                                        vertical-align: top;" nowrap="true">
                                        <span class="over" id="stationtitle" onmouseover="titleover('stationtitle');" onmouseout="titleout('stationtitle');"
                                            onclick="titleclick('stationtitle');" style="text-align: center; width: 50%; cursor:hand;">岗位人员</span><span
                                                id="grouptitle" style="text-align: center; width: 50%;  cursor:hand;" onmouseover="titleover('grouptitle');"
                                                onmouseout="titleout('grouptitle')" onclick="titleclick('grouptitle');">分组人员</span>
                                    </div>
                                    <iframe name="frameYLeft" id="frameYLeft" src="SelectSingleGroup.aspx" height="90%" scrolling="auto" width="100%" style="border:0; display:none;"></iframe>
                                    <table id="Table3" bordercolor="#e7e7e7" cellspacing="0" cellpadding="3" width="100%"
                                        border="0">
                                        <tbody id="Tree">
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <iframe id="frameMain" height="98%" width="100%" src="" frameborder="0"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="10">
                        <tr>
                            <td rowspan="2" align="center">
                                <input class="submit" id="btnClear" type="button" value="清 除" onclick="ClearClick();" />
                                <input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="txtFrom" type="hidden" name="txtFrom" runat="server" />
        <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" />
        <input id="txtRootUnitCode" type="hidden" name="txtRootUnitCode" runat="server" />
    </form>

    <script type="text/javascript">
    
        <!-- Hide 
function killErrors() { 
return true; 
} 
window.onerror = killErrors; 
// --> 
    
    var tempID = 'stationtitle';
	
	function titleover(eleid)
	{
	    if(eleid != tempID)
	    {
	        document.getElementById(eleid).className='over';
	        document.getElementById(tempID).className='default';
	    }
	}
	
	function titleout(eleid)
	{
	    if(eleid != tempID)
	    {
	        document.getElementById(eleid).className='default';
	        document.getElementById(tempID).className='over';
	    }
	}
	
	function titleclick(eleid)
	{
	    if(eleid != tempID)
	    {
	        if(eleid == 'grouptitle')
	        {
	            document.all.frameYLeft.style.display ='block';
	            document.all.Table3.style.display ='none';
	            document.all.frameMain.src ='';
	        }
	        else
	        {
	            document.all.frameYLeft.style.display ='none';
	            document.all.Table3.style.display ='block';
	            document.all.frameMain.src ='';
	        }
	        tempID = eleid;
	    }
	
	}
	
	function setuservalue(usercode,username)
	{
	    var flag = '<%=Request["Flag"]%>';
		window.opener.<%=ViewState["ReturnFunc"]%>(usercode,username,flag);
		window.close();
	}

        // 显示部门
        function showDepartment(obj, layer, parentCode, NodeType){
	        var id = obj.id;
	        var code = DecodeId(id);
	        //此屏幕只做单选用户 2004.11.26
        //	document.all("frameMain").src = 'UserList.aspx?Type=Single&ProjectCode=<%=Request["ProjectCode"]%>&Flag=<%=Request["Flag"]%>&UnitCode='+ code+"&Identity=<%=Request["Identity"]%>&ReturnFunc=<%=ViewState["ReturnFunc"]%>";
	        document.all("frameMain").src = 'UserList.aspx?Type=<%=Request["Type"]%>&ProjectCode=<%=Request["ProjectCode"]%>&Flag=<%=Request["Flag"]%>&UnitCode='+ code+"&Identity=<%=Request["Identity"]%>&ReturnFunc=<%=ViewState["ReturnFunc"]%>";
        }

        function showStation(obj, layer, parentCode){
	        var id = obj.id;
	        var code = DecodeId(id);
	        //此屏幕只做单选用户 2004.11.26
        //	document.all("frameMain").src ='UserList.aspx?Type=Single&ProjectCode=<%=Request["ProjectCode"]%>&Flag=<%=Request["Flag"]%>&StationCode=' + code + '&UnitCode=' + DecodeId(parentCode) +"&Identity=<%=Request["Identity"]%>&ReturnFunc=<%=ViewState["ReturnFunc"]%>";
	        document.all("frameMain").src ='UserList.aspx?Type=<%=Request["Type"]%>&ProjectCode=<%=Request["ProjectCode"]%>&Flag=<%=Request["Flag"]%>&StationCode=' + code + '&UnitCode=' + DecodeId(parentCode) +"&Identity=<%=Request["Identity"]%>&ReturnFunc=<%=ViewState["ReturnFunc"]%>";
        }

        //Tree
        var TreeObj=document.all("Tree");
        var RowClassName="ListBodyTr1";
        var GridClassName="TreeViewItemTd";

        var DataSourceUrl='../Systems/OBSData.aspx?TreeType=';

        // @IndentStart			缩进内容循环开始点
        // @IndentEnd			缩进内容循环结束点
        // @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
        // @NodeSymbolEnd		节点标志开始点
        // @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
        // @JsCodeEnd			节点标志开始点

        //部门
        var TreeModels=new Array();
        var v0 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
        v0+="<td onclick=\"SpreadUnitNodes('@Code','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
        v0+="<td style=\"CURSOR: hand\" id=\"@Code\" onclick=\"TDClick(this);showDepartment(this, '@Layer', '@ParentCode', '@NodeType');return false;\"><img src=\"../Images/@ImageName\">@Name (@UserCount)</td></tr></table>";
        //v0+="<td><img src=\"../Images/@ImageName\"><a href=\"#\" id=\"@Code\" onclick=\"ShowEditMenu(this, '@Layer', '@ParentCode', '@NodeType');return false;\">@Name</a></td></tr></table>";
        TreeModels.push(v0);

        //岗位
        var TreeModelsRole=new Array();
        var v1 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
        v1+="<td  width=\"20\" align=\"center\" >@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
        v1+="<td style=\"CURSOR: hand\" id=\"@Code\" onclick=\"TDClick(this);showStation(this, '@Layer', '@ParentCode');return false;\"><img src=\"../Images/group.gif\">@Name (@UserCount)</td></tr></table>";
        TreeModelsRole.push(v1);



        //展开部门
        function SpreadUnitNodes(UnitCode,LayerNumber,obj){
	        var node = obj;

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
				        GetChildNodes(DataSourceUrl+"&NotDisplayNull=1&GetType=ChildNodesRoleOfUnit&NodeId="+UnitCode + "&ParentLayer=" + LayerNumber,node,TreeModelsRole,"Code",RowClassName,GridClassName);
				        GetChildNodes(DataSourceUrl+"&NotDisplayNull=1&GetType=ChildNodes&NodeId="+UnitCode + "&ParentLayer=" + LayerNumber,node,TreeModels,"Code",RowClassName,GridClassName);
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



        //更新部门
        function updateUnit(NodeId, Layer){
	        RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId+"&CurrLayer=" + Layer,TreeObj,NodeId,TreeModels,"Code",RowClassName,GridClassName);
        }

        //更新岗位 
        function updateRole(NodeId, Layer){
	        RefreshNode(DataSourceUrl+"&GetType=SingleNodeRole&NodeId="+NodeId+"&CurrLayer=" + Layer,TreeObj,NodeId,TreeModelsRole,"Code",RowClassName,GridClassName);
        }


        //更新部门子节点
        function updateUnitChildNodes(parentNodeId,Layer){
	        if (parentNodeId == "") {
		        RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+ Form1.txtRootUnitCode.value +"&ParentLayer="+Layer,TreeObj,parentNodeId,TreeModels,"Code",RowClassName,GridClassName);
	        }
	        else {

	        //-------------begin
	        var NodeId = parentNodeId;
        	
	        var obj=null;
	        for(var i=0;i<TreeObj.childNodes.length;i++){
		        if(TreeObj.childNodes[i].NodeId==NodeId){
			        obj=TreeObj.childNodes[i];
			        break;
		        }
	        }
        	
	        if(obj==null){
		        RemoveAllChildNode(TreeObj);
        //		GetChildNodes(url,null,Models,keyField,RowClassName,GridClassName);
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
        		
		        var itemCount2=GetChildNodes(DataSourceUrl+"&GetType=ChildNodesRoleOfUnit&NodeId="+parentNodeId+"&ParentLayer="+Layer+"&Layer="+obj.NodeLayer,obj,TreeModelsRole,"Code",RowClassName,GridClassName);
		        if(itemCount2){
			        InsertOpendNodeKeys(obj.NodeId);
		        }

		        var itemCount1=GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&ParentLayer="+Layer+"&Layer="+obj.NodeLayer,obj,TreeModels,"Code",RowClassName,GridClassName);
		        if(itemCount1){
			        InsertOpendNodeKeys(obj.NodeId);
		        }

		        if(itemCount1 || itemCount2){
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
	        //-------------end

	        }
        }


        //展开某个节点
        function expandNode(node) {
	        var id, layer, type;
        	
	        if ((node == undefined) || (node == null))
		        return;

	        if (node.NodeStatus == "Closed")
	        {
		        id = node.NodeId;
		        layer = node.NodeLayer;
		        type = GetNodeType(id);
        		
		        if (type == "D") {
			        SpreadUnitNodes(id, layer, node);
		        }

	        }
        }



        //展开当前节点
        function expandSingle() {
	        if (clicknode == undefined)
		        return;
        		
	        var origin_id = clicknode.id;
        	
	        var tree = document.all.Tree;
	        var i;
	        var id;
	        var layer;
	        var type;

	        node = FindNode(tree, origin_id);
	        if (node == null)
		        return;
        	
	        var origin_layer = node.NodeLayer;

	        while ((node != undefined) && (node != null))
	        {
		        if ((node.NodeId != origin_id) && (node.NodeLayer <= origin_layer))
			        return;
        			
		        expandNode(node);
		        node = node.nextSibling;
	        }
        }

        function CreateTree()
        {
	        var PreCount = 0;
	        RemoveAllChildNode(document.all.Tree);
        	
	        var rootCode = Form1.txtRootUnitCode.value;

	        if (rootCode != "")
	        {
		        GetChildNodes(DataSourceUrl+"&GetType=SingleNode&NodeId=" + rootCode,null,TreeModels,"Code",RowClassName,GridClassName);
	        //	SpreadUnitNodes(rootCode, 2, document.all.Tree.childNodes[0]);
	        }
	        else
	        {
		        //根结点加“未定岗人员”
		        GetChildNodes(DataSourceUrl+"&GetType=NoStationUser&NotDisplayNull=1&CurrLayer=1",null,TreeModels,"Code",RowClassName,GridClassName);
		        PreCount = document.all.Tree.childNodes.length;
        	
		        GetChildNodes(DataSourceUrl+"&NodeId=" + rootCode,null,TreeModels,"Code",RowClassName,GridClassName);
	        }

	        if (document.all.Tree.childNodes.length > PreCount) {
		        SpreadUnitNodes(document.all.Tree.childNodes[PreCount].NodeId, document.all.Tree.childNodes[PreCount].NodeLayer, document.all.Tree.childNodes[PreCount]);
	        }
        }

        CreateTree();

        //清除
        function ClearClick()
        {
	        var flag = '<%=Request["Flag"]%>';
	        window.opener.<%=ViewState["ReturnFunc"]%>("","",flag);
	        window.close();
        }
	
    </script>

</body>
</html>

